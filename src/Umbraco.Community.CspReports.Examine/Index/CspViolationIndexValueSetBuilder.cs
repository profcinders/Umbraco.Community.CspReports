using Examine;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Community.CspReports.Models;

namespace Umbraco.Community.CspReports.Examine.Index
{
    public class CspViolationIndexValueSetBuilder : IValueSetBuilder<Report>
    {
        public IEnumerable<ValueSet> GetValueSets(params Report[] content)
        {
            foreach (var report in content.Where(report => report.Body != null))
            {
                var indexValues = new Dictionary<string, object>
                {
                    [UmbracoExamineFieldNames.NodeNameFieldName] = report.Body!.BlockedUrl!,

                    [nameof(Report.DateReceived)] = report.DateReceived,
                    [nameof(CspViolation.SourceFile)] = report.Body.SourceFile ?? string.Empty,
                    [nameof(CspViolation.LineNumber)] = report.Body.LineNumber ?? 0,
                    [nameof(CspViolation.ColumnNumber)] = report.Body.ColumnNumber ?? 0,
                    [nameof(CspViolation.DocumentUrl)] = report.Body.DocumentUrl ?? string.Empty,
                    [nameof(CspViolation.Referrer)] = report.Body.Referrer ?? string.Empty,
                    [nameof(CspViolation.BlockedUrl)] = report.Body.BlockedUrl ?? string.Empty,
                    [nameof(CspViolation.EffectiveDirective)] = report.Body.EffectiveDirective ?? string.Empty,
                    [nameof(CspViolation.OriginalPolicy)] = report.Body.OriginalPolicy ?? string.Empty,
                    [nameof(CspViolation.Sample)] = report.Body.Sample ?? string.Empty,
                    [nameof(CspViolation.Disposition)] = report.Body.Disposition ?? string.Empty,
                    [nameof(CspViolation.StatusCode)] = report.Body.StatusCode ?? 0,
                };

                yield return new ValueSet(Guid.NewGuid().ToString(), CspReportsExamineConstants.ReportCategory, indexValues);
            }
        }
    }
}
