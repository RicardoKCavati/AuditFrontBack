using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Repositories;

namespace AuditApp.Server.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuditContext _auditContext;

        public UserRepository(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public UserModel? FindByEmail(string email)
        {
            return _auditContext.Users.FirstOrDefault(user => user.Email.Equals(email));
        }

        public void SaveUser(UserModel userModel)
        {
            _auditContext.Users.Add(userModel);

            _auditContext.SaveChanges();
        }

        public void UpdateUserPasswordCode(string email, int passwordCode, DateTime expirationDate)
        {
            var user = FindByEmail(email);

            if (user != null)
            {
                user.ChangePasswordToken = passwordCode;
                user.ExpirationDate = expirationDate;
                _auditContext.Users.Update(user);
                _auditContext.SaveChanges();
                return;
            }

            throw new Exception("Cadastro não encontrado, verifique seu e-mail");
        }

        public void UpdatePassword(string email, string newPassword)
        {
            var user = FindByEmail(email);

            if (user != null)
            {
                user.ChangePasswordToken = 0;
                user.ExpirationDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                user.Password = newPassword;
                _auditContext.Users.Update(user);
                _auditContext.SaveChanges();
                return;
            }

            throw new Exception("Cadastro não encontrado, verifique seu e-mail");
        }
    }
}