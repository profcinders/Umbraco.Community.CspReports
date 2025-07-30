namespace Umbraco.Community.CspReports.Models
{
    public class CspViolation
    {
        public string? SourceFile { get; set; }
        public int? LineNumber { get; set; }
        public int? ColumnNumber { get; set; }
        public string? DocumentUrl { get; set; }
        public string? Referrer { get; set; }
        public string? BlockedUrl { get; set; }
        public string? EffectiveDirective { get; set; }
        public string? OriginalPolicy { get; set; }
        public string? Sample { get; set; }
        public string? Disposition { get; set; }
        public int? StatusCode { get; set; }
    }
}
