using AuditApp.Server.Services;
using AuditApp.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using SecurityDriven.Inferno;

namespace AuditApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly EmailService _emailService;

        public PasswordController(UserService userService, EmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("SendResetPasswordCode")]
        public ActionResult SendResetPasswordCode([FromBody] string email)
        {
            try
            {
                var changePasswordCode = new CryptoRandom().Next(100_000, 999_999);

                var expirationDate = DateTime.Now.AddMinutes(5);

                _userService.UpdatePasswordCodeAndExpirationDate(email, changePasswordCode, expirationDate);

                _emailService.SendResetPasswordEmailMail(email, changePasswordCode, expirationDate);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("ValidateCode")]
        public ActionResult ValidateCode(CodeValidation validation)
        {
            try
            {
                var user = _userService.GetUserByEmail(validation.Email);

                if (user.ChangePasswordToken != validation.Code)
                {
                    return StatusCode(500, "Código inválido");
                }

                if (DateTime.Now > user.ExpirationDate)
                {
                    return StatusCode(500, "Código expirado, solicite outro");
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(PasswordChange validation)
        {
            try
            {
                var user = _userService.GetUserByEmail(validation.Email);

                _userService.UpdatePassword(validation.Email, validation.Password);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
