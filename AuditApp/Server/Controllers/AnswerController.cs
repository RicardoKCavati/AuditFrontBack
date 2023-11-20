using AuditApp.Server.Services;
using AuditApp.Shared.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AuditApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly AnswerService _answerService;

        public AnswerController(AnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        [Route("SaveQuestionAnswered")]
        public ActionResult SaveQuestionAnswered(AnsweredQuestion answeredQuestion)
        {
            try
            {
                _answerService.HandleAnswer(answeredQuestion);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
