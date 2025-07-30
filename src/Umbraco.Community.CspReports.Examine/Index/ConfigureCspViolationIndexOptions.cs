using Examine;
using Examine.Lucene;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Util;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Community.CspReports.Models;

namespace Umbraco.Community.CspReports.Examine.Index
{
    internal class ConfigureCspViolationIndexOptions(IOptions<IndexCreatorSettings> _settings)
        : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
    {
        public void Configure(string? name, LuceneDirectoryIndexOptions options)
        {
            if (name?.Equals(CspReportsExamineConstants.IndexName) is not true)
            {
                return;
            }

            options.Analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);

            options.FieldDefinitions = new(
                new(nameof(Report.DateReceived), FieldDefinitionTypes.DateTime),
                new(nameof(CspViolation.LineNumber), FieldDefinitionTypes.Integer),
                new(nameof(CspViolation.ColumnNumber), FieldDefinitionTypes.Integer),
                //new(nameof(CspViolation.OriginalPolicy), FieldDefinitionTypes.Raw), // Not sure if this is necessary?
                new(nameof(CspViolation.StatusCode), FieldDefinitionTypes.Integer));

            options.UnlockIndex = true;

            if (_settings.Value.LuceneDirectoryFactory == LuceneDirectoryFactory.SyncedTempFileSystemDirectoryFactory)
            {
                options.IndexDeletionPolicy = new SnapshotDeletionPolicy(new KeepOnlyLastCommitDeletionPolicy());
            }
        }

        public void Configure(LuceneDirectoryIndexOptions options)
            => throw new NotImplementedException();
    }
}
