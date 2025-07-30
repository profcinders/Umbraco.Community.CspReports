using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Community.CspReports.Data;
using Umbraco.Community.CspReports.Models;

namespace Umbraco.Community.CspReports.ReportCollection
{
    [ApiController]
    [Route($"/umbraco/api/{CspReportsConstants.PluginAlias}")]
    public class ReportApiController(ICspReportsService _reportsService, ILogger<ReportApiController> _logger) : Controller
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] List<Report> reports)
        {
            try
            {
                await _reportsService.AddAsync(reports);
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    _logger.LogError(innerEx, "Error occurred while adding CSP violation");
                }
            }

            return Ok();
        }
    }
}
