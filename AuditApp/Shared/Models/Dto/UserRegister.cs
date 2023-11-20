using System.Globalization;

namespace AuditApp.Shared.Models.Dto
{
    public class UserRegister
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name{ get; set; } = string.Empty;
    }
}
