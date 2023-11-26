using AuditApp.Server.Objects;
using AuditApp.Server.Services;
using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Business;
using AuditApp.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AuditApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly UserService _userService;
        private readonly EmailService _emailService;
        private readonly QuestionService _questionService;

        public AuthController(JwtTokenHandler jwtTokenHandler,
                              UserService userService,
                              EmailService emailService,
                              QuestionService questionService)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _userService = userService;
            _emailService = emailService;
            _questionService = questionService;
        }

        private void GetAll(string line, out string question, out string requirement, out string standard)
        {
            var result = line.Split('|');
            question = result[0];
            standard = result[1];
            requirement = result[2];
        }

        [HttpGet]
        [Route("getfake")]
        public ActionResult FillQuestions()
        {
            var questions = new List<QuestionDb>();
            try
            {
                const Int32 BufferSize = 1024;
                using var fileStream = System.IO.File.OpenRead("C:\\Users\\Ricardo\\Downloads\\Perguntas_Nildao.txt");
                using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);
                string line;

                line = streamReader.ReadLine();

                while (!string.IsNullOrEmpty(line))
                {
                    try
                    {
                        GetAll(line, out var question, out var requirement, out var standard);
                        questions.Add(new QuestionDb
                        {
                            Question = question,
                            Requirements = requirement,
                            Standards = standard
                        });

                        line = streamReader.ReadLine();
                    }
                    catch (Exception e)
                    {

                    }
                }

                _questionService.AddRangeOfQuestions(questions);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
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
