namespace Umbraco.Community.CspReports.Models
{
    public class Report
    {
        public string? Type { get; set; } = "csp-violation";
        public string? Url { get; set; }
        public CspViolation? Body { get; set; }

        public DateTime DateReceived { get; set; } = DateTime.UtcNow;
    }
}
