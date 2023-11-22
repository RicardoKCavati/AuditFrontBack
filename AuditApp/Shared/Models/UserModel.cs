using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuditApp.Shared.Models
{
    [Table("Users")]
    public class UserModel
    {
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("ChangePasswordToken")]
        public int ChangePasswordToken { get; set; }

        [JsonPropertyName("ExpirationDate")]
        public DateTime ExpirationDate { get; set; }
    }
}
