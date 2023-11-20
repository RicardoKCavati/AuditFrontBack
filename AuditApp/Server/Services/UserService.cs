using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Repositories;

namespace AuditApp.Server.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel? GetUserByEmail(string email)
        {
            return _userRepository.FindByEmail(email);
        }

        public void AddNewUser(UserModel userModel)
        {
            _userRepository.SaveUser(userModel);
        }

        public void UpdatePasswordCodeAndExpirationDate(string email, int changePasswordCode, DateTime expirationDate)
        {
            _userRepository.UpdateUserPasswordCode(email, changePasswordCode, expirationDate);
        }

        public void UpdatePassword (string email, string newPassword)
        {
            _userRepository.UpdatePassword(email, newPassword);
        }
    }
}
