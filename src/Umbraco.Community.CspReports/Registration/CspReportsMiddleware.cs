using Microsoft.AspNetCore.Http;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Community.CspReports.Registration
{
    public class CspReportsMiddleware(
        RequestDelegate _next,
        IRuntimeState _runtimeState)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            if (_runtimeState.Level is not Cms.Core.RuntimeLevel.Run)
            {
                await _next(context);
                return;
            }

            context.Response.OnStarting(() =>
            {
                context.Response.Headers["Reporting-Endpoints"] = $"csp-endpoint=\"https://{context.Request.Host.Value}/umbraco/api/{CspReportsConstants.PluginAlias}/add\"";
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
