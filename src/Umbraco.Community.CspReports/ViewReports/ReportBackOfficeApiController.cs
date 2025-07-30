using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Community.CspReports.Data;

namespace Umbraco.Community.CspReports.ViewReports
{
    [ApiController]
    [Route($"/umbraco/backoffice/api/{CspReportsConstants.PluginAlias}")]
    public class ReportBackOfficeApiController(ICspReportsService _reportsService, ILogger<ReportBackOfficeApiController> _logger) : Controller
    {
        [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
        [HttpGet("get")]
        public async Task<IActionResult> Get(long page = 1, int pageSize = 20)
        {
            if (pageSize < 1) return BadRequest("Must request a positive page size");
            if (page < 1) page = 1;

            try
            {
                return Ok(await _reportsService.GetPagedAsync(page, pageSize));
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    _logger.LogError(innerEx, "Error occurred while retrieving CSP violations");
                }

                return StatusCode(500);
            }
        }

        [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
        [HttpDelete("deleteall")]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                await _reportsService.DeleteAll();
            }
            catch (AggregateException ex)
            {
                foreach (var innerEx in ex.InnerExceptions)
                {
                    _logger.LogError(innerEx, "Error occurred while deleting all CSP violations");
                }

                return StatusCode(500);
            }

            return Ok();
        }
    }
}
