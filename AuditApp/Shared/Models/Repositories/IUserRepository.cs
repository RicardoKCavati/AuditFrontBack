namespace AuditApp.Shared.Models.Repositories
{
    public interface IUserRepository
    {
        UserModel? FindByEmail(string email);
        void SaveUser(UserModel userModel);
        void UpdateUserPasswordCode(string email, int passwordCode, DateTime expirationDate);
        void UpdatePassword(string email, string newPassword);
    }
}
