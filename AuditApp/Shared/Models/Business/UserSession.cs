namespace AuditApp.Shared.Models.Business
{
    public class UserSession
    {
        public UserSession(string email, string token, string role, int expiresIn, DateTime expiryTimesStamp, string name)
        {
            Email = email;
            Token = token;
            Role = role;
            ExpiresInSeconds = expiresIn;
            ExpirationDate = expiryTimesStamp;
            Name = name;
        }

        public UserSession()
        {
            
        }

        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int ExpiresInSeconds { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
