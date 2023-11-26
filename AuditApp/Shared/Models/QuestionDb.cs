using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuditApp.Shared.Models
{
    [Table("Questions")]
    public class QuestionDb
    {
        [JsonPropertyName("QuestionId")]
        public int QuestionId { get; set; }
        [JsonPropertyName("Requirements")]
        public string Requirements { get; set; } = string.Empty;
        [JsonPropertyName("Question")]
        public string Question { get; set; } = string.Empty;
        [JsonPropertyName("Standards")]
        public string Standards { get; set; } = string.Empty;
    }
}
