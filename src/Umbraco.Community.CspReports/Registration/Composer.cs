using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace Umbraco.Community.CspReports.Registration
{
    public sealed class Composer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddManifestFilter<ManifestFilter>();

            builder.Services.Configure<UmbracoPipelineOptions>(options =>
            {
                options.AddFilter(new UmbracoPipelineFilter(
                    CspReportsConstants.PackageAlias,
                    postPipeline: applicationBuilder =>
                    {
                        applicationBuilder.UseMiddleware<CspReportsMiddleware>();
                    }));
            });
        }
    }
}
