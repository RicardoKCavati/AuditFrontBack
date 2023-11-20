using System.Text.Json.Serialization;

namespace AuditApp.Shared.Models.Requests
{
    public class LoginRequest
    {
        [JsonPropertyName("Email")]
        public string Email { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
