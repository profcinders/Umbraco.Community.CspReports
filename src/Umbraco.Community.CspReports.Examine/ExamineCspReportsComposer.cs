using Examine;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Community.CspReports.Data;
using Umbraco.Community.CspReports.Examine.Index;

namespace Umbraco.Community.CspReports.Examine
{
    public class ExamineCspReportsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddExamineLuceneIndex<CspViolationIndex, ConfigurationEnabledDirectoryFactory>(nameof(CspViolationIndex));
            builder.Services.ConfigureOptions<ConfigureCspViolationIndexOptions>();
            builder.Services.AddSingleton<CspViolationIndexValueSetBuilder>();
            builder.Services.AddSingleton<IIndexPopulator, CspViolationIndexPopulator>();

            builder.Services.AddTransient<ICspReportsService, ExamineCspReportsService>();
        }
    }
}
