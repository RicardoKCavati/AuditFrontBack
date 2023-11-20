using AuditApp.Server.Objects;
using AuditApp.Server.Services;
using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Business;
using AuditApp.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AuditApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public AuthController(JwtTokenHandler jwtTokenHandler,
                              UserService userService,
                              EmailService emailService)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<UserSession> Login(UserLogin login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                {
                    return StatusCode(500, "Preencha todas as informações necessárias");
                }

                var userModel = _userService.GetUserByEmail(login.Email);

                if (userModel == null || userModel.Password != login.Password)
                {
                    return Unauthorized("Usuário não encontrado, verifique e-mail e senha");
                }

                var userSession = _jwtTokenHandler.GenerateJwtToken(userModel);

                if (userSession is not null)
                {
                    _emailService.SendMfaCodeToLogin(userSession.Email, out var mfaCode, out var expirationDate);

                    _userService.UpdatePasswordCodeAndExpirationDate(userSession.Email, mfaCode, expirationDate);

                    return userSession;
                }

                return Unauthorized("Usuário não encontrado, verifique e-mail e senha");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(UserRegister register)
        {
            try
            {
                if (string.IsNullOrEmpty(register.Email) || string.IsNullOrEmpty(register.Password) || string.IsNullOrEmpty(register.Name))
                {
                    return StatusCode(500, "Preencha todas as informações necessárias");
                }

                var userModel = new UserModel()
                {
                    Email = register.Email,
                    Password = register.Password,
                    Name = register.Name,
                    Role = "User"
                };

                _userService.AddNewUser(userModel);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
