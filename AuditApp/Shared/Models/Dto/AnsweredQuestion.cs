namespace AuditApp.Shared.Models.Dto
{
    public class AnsweredQuestion
    {
        public string CompanyId { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public bool Complies { get; set; }
    }
}
