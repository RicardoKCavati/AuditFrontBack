using System.ComponentModel.DataAnnotations.Schema;

namespace AuditApp.Shared.Models
{
    [Table("Answers")]
    public class AnswerModel
    {
        public int QuestionId { get; set; }
        public string CompanyId { get; set; } = string.Empty;
        public bool IsInCompliance { get; set; }
    }
}
