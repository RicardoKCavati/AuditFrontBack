using AuditApp.Server.Services;
using AuditApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuditApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly AnswerService _answerService;

        public QuestionController(AnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpPost]
        [Route("ObtainQuestions")]
        public ActionResult<List<QuestionModel>> ObtainQuestions([FromBody] string cnpj)
        {
            var questions = new List<QuestionModel>();
            var answers = _answerService.GetAllAnswers();

            if (answers == null)
            {
                answers = new List<AnswerModel>();
            }

            var dbQuestions = new List<QuestionDb>();
            for (int i = 0; i < 1000; i++)
            {

                dbQuestions.Add(new QuestionDb
                {
                    QuestionId = i,
                    Question = $"Question number{i}",
                    Standard = $"Standard number{i}",
                    Requirement = $"Requirement number{i}",
                });
            }

            foreach (var question in dbQuestions)
            {
                var ans = answers.FirstOrDefault(a => a.QuestionId == question.QuestionId && a.CompanyId.Equals(cnpj));

                var isInCompliance = false;

                if (ans != null)
                {
                    isInCompliance = ans.IsInCompliance;
                }

                questions.Add(new QuestionModel
                {
                    QuestionId = question.QuestionId,
                    IsInCompliance = isInCompliance,
                    Question = question.Question,
                    Requirement = question.Requirement,
                    Standard = question.Standard,
                    State = isInCompliance ? "Atende" : "Não atende",
                    CompanyId = cnpj
                });
            }

            return questions;
        }
    }
}
