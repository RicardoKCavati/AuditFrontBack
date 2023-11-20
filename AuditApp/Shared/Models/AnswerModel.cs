namespace AuditApp.Shared.Models
{
    public class AnswerModel
    {
        public int QuestionId { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public bool IsInCompliance { get; set; }
    }
}
