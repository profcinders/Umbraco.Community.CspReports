using Umbraco.Community.CspReports.Models;

namespace Umbraco.Community.CspReports.Data
{
    public class GetPagedResponse
    {
        public IEnumerable<Report> Results { get; set; } = [];
        public long TotalResults { get; set; }
        public long Page { get; set; }
        public long TotalPages { get; set; }
    }
}
