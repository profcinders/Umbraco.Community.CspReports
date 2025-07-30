using Umbraco.Community.CspReports.Models;

namespace Umbraco.Community.CspReports.Data
{
    public interface ICspReportsService
    {
        Task AddAsync(IEnumerable<Report> report);

        Task<GetPagedResponse> GetPagedAsync(long page, int pageSize);

        Task DeleteAll();
    }
}
