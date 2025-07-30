using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.CspReports.Registration
{
    internal sealed class ManifestFilter : IManifestFilter
    {
        public void Filter(List<PackageManifest> manifests)
        {
            manifests.Add(new PackageManifest()
            {
                PackageName = CspReportsConstants.PackageAlias,
                PackageId = CspReportsConstants.PackageAlias,
                Scripts =
                [
                    $"/App_Plugins/{CspReportsConstants.PluginAlias}/backoffice/CspReports-list/reports.resource.js",
                    $"/App_Plugins/{CspReportsConstants.PluginAlias}/backoffice/CspReports-list/reports-list.controller.js",
                ],
                Version = typeof(ManifestFilter)?.Assembly?.GetName()?.Version?.ToString(3) ?? string.Empty,
                AllowPackageTelemetry = true,
            });
        }
    }
}
