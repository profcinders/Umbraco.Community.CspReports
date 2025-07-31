# Umbraco.Community.CspReports

[![GitHub License](https://img.shields.io/github/license/profcinders/Umbraco.Community.CspReports?style=for-the-badge)](https://github.com/profcinders/Umbraco.Community.CspReports/blob/develop/LICENSE)

An Umbraco plugin to store, display, and filter reports received from Content-Security-Policy headers

## Installation

Nuget packages will be available Soon&trade;.

## Configuration

After installation, visit the CSP Manager section in the Umbraco back office. If you haven't already, you can set up the CSPs for the back office and front end with Matt Wise's wonderful [CSP Manager plugin](https://github.com/Matthew-Wise/Umbraco-CSP-manager), which this plugin depends on. You may wish to set these to "Report only" until you're happy with your policies.

In the settings for both the Back Office and Front end CSPs, set "Toggle reporting" to `report-to` and "Report value" to `csp-endpoint`.

> [!IMPORTANT]
> This plugin currently only supports the `report-to` option. Not all browsers may support this option yet, so please check its [compatibility](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Content-Security-Policy/report-to#browser_compatibility).

## Usage

To view a list of all reports, go to the CSP Manager section in Umbraco back office. From here, visit the "CSP Reports" option in the tree. You should see a paged list of all reported violations of your set policies, along with options to **Refresh** the list and **Delete all** records.

The **Refresh page** button allows you to reload the current page in case you experience issues.

**Delete all** will do exactly that - delete **all** records, irretrievably. Be careful when doing this, as there's no going back!

## Extending

While there is currently only a package for Examine, you can also create your own reports service to use whatever method you like to store and retrieve CSP reports.

The key is to implement the `ICspReportsService`. This allows the reporting API to pass the records to and from your storage solution. For an example on how to implement this, check out the [ExamineCspReportsService](src/Umbraco.Community.CspReports.Examine/ExamineCspReportsService.cs).

You will also need to add your implementation as a service via a composer. Ensure that only one such service is added - don't install the Examine package if you intend to write your own!

```csharp
public class MyCspReportsComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<Umbraco.Community.CspReports.Data.ICspReportsService, MyCspReportsService>();
    }
}
```
