using System.Text.Json.Serialization;

namespace AuditApp.Shared.Models.Responses
{
    public class GradeResponse
    {
        [JsonPropertyName("QuestionCount")]
        public int QuestionCount { get; set; }
        [JsonPropertyName("AnsweredCount")]
        public int AnsweredCount { get; set; }
        [JsonPropertyName("UnansweredCount")]
        public int UnansweredCount { get; set; }

        [JsonPropertyName("InComplianceQuestions")]
        public int InComplianceQuestions { get; set; }
        [JsonPropertyName("NotInComplianceQuestions")]
        public int NotInComplianceQuestions { get; set; }
    }
}
