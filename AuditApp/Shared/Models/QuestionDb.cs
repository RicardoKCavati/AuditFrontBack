using System.ComponentModel.DataAnnotations.Schema;

namespace AuditApp.Shared.Models
{
    [Table("Questions")]
    public class QuestionDb
    {
        public int QuestionId { get; set; }
        public string Requirement { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
        public string Standard { get; set; } = string.Empty;
    }
}
