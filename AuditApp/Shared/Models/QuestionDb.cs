using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuditApp.Shared.Models
{
    [Table("Questions")]
    public class QuestionDb
    {
        [JsonPropertyName("QuestionId")]
        public int QuestionId { get; set; }
        [JsonPropertyName("Requirement")]
        public string Requirement { get; set; } = string.Empty;
        [JsonPropertyName("Question")]
        public string Question { get; set; } = string.Empty;
        [JsonPropertyName("Standard")]
        public string Standard { get; set; } = string.Empty;
    }
}
