using Examine;
using Microsoft.Extensions.Logging;
using Umbraco.Community.CspReports.Data;
using Umbraco.Community.CspReports.Examine.Index;
using Umbraco.Community.CspReports.Models;
using ExamineSearch = Examine.Search;

namespace Umbraco.Community.CspReports.Examine
{
    public class ExamineCspReportsService(
        IExamineManager _examineManager,
        CspViolationIndexValueSetBuilder _valueSetBuilder,
        ILogger<ExamineCspReportsService> _logger)
        : ICspReportsService
    {
        public Task AddAsync(IEnumerable<Report> report)
        {
            GetIndex().IndexItems(_valueSetBuilder.GetValueSets([.. report]));
            return Task.CompletedTask;
        }

        public Task DeleteAll()
        {
            GetIndex().CreateIndex();
            return Task.CompletedTask;
        }

        public Task<GetPagedResponse> GetPagedAsync(long page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            if (skip > int.MaxValue)
            {
                _logger.LogError("Too many CSP violations");
                throw new InvalidOperationException("Too many CSP violations");
            }

            var query = GetIndex().Searcher
                .CreateQuery(CspReportsExamineConstants.ReportCategory)
                .All()
                .OrderByDescending(new ExamineSearch.SortableField(nameof(Report.DateReceived), ExamineSearch.SortType.Long));
            var results = query.Execute(new((int)skip, pageSize));

            return Task.FromResult(new GetPagedResponse
            {
                Results = results.Select(ConvertToReport),
                TotalResults = results.TotalItemCount,
                Page = page,
                TotalPages = Convert.ToInt64(Math.Ceiling(results.TotalItemCount / (decimal)pageSize)),
            });
        }

        private IIndex GetIndex()
        {
            if (!_examineManager.TryGetIndex(CspReportsExamineConstants.IndexName, out var index))
            {
                _logger.LogError("Could not obtain the {IndexName}", CspReportsExamineConstants.IndexName);
                throw new InvalidOperationException($"Could not obtain the {CspReportsExamineConstants.IndexName}");
            }

            return index;
        }

        private Report ConvertToReport(ISearchResult indexResult)
        {
            var received = DateTime.MinValue;
            if (long.TryParse(indexResult[nameof(Report.DateReceived)], out var ticks))
            {
                received = new DateTime(ticks);
            }

            return new Report
            {
                Url = indexResult[nameof(CspViolation.BlockedUrl)],
                Body = new CspViolation
                {
                    SourceFile = indexResult[nameof(CspViolation.SourceFile)],
                    LineNumber = int.Parse(indexResult[nameof(CspViolation.LineNumber)]),
                    ColumnNumber = int.Parse(indexResult[nameof(CspViolation.ColumnNumber)]),
                    DocumentUrl = indexResult[nameof(CspViolation.DocumentUrl)],
                    Referrer = indexResult[nameof(CspViolation.Referrer)],
                    BlockedUrl = indexResult[nameof(CspViolation.BlockedUrl)],
                    EffectiveDirective = indexResult[nameof(CspViolation.EffectiveDirective)],
                    OriginalPolicy = indexResult[nameof(CspViolation.OriginalPolicy)],
                    Sample = indexResult[nameof(CspViolation.Sample)],
                    Disposition = indexResult[nameof(CspViolation.Disposition)],
                    StatusCode = int.Parse(indexResult[nameof(CspViolation.StatusCode)]),
                },
                DateReceived = received,
            };
        }
    }
}
