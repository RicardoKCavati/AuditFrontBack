namespace AuditApp.Shared.Models
{
    public class QuestionDb
    {
        public int QuestionId { get; set; }
        public string Requirement { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string Standard { get; set; } = string.Empty;
    }
}
