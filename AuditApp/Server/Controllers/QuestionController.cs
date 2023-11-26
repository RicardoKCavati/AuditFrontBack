using AuditApp.Server.Services;
using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AuditApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly AnswerService _answerService;
        private readonly QuestionService _questionService;

        public QuestionController(AnswerService answerService, QuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }

        [HttpPost]
        [Route("ObtainQuestionStatus")]
        public ActionResult<GradeResponse> ObtainQuestionStatus([FromBody] string cnpj)
        {
            try
            {
                var questionCount = _questionService.GetAllQuestionsCount();

                var answeredQuestions = _answerService.GetAnswersByCompanyId(cnpj);

                var inCompliance = answeredQuestions.Count(answers => answers.IsInCompliance);

                var isNotInCompliance = answeredQuestions.Count(answers => !answers.IsInCompliance);

                var unansweredQuestions = questionCount - answeredQuestions.Count;

                return Ok(new GradeResponse
                {
                    AnsweredCount = answeredQuestions.Count,
                    InComplianceQuestions = inCompliance,
                    NotInComplianceQuestions = isNotInCompliance,
                    QuestionCount = questionCount,
                    UnansweredCount = unansweredQuestions
                });
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("ObtainQuestions")]
        public ActionResult<List<QuestionModel>> ObtainQuestions([FromBody] string companyId)
        {
            var questions = new List<QuestionModel>();
            var answers = _answerService.GetAllAnswers();

            if (answers == null)
            {
                answers = new List<AnswerModel>();
            }

            var dbQuestions = _questionService.GetAllQuestionsFromDatabase();
            //TODO BUSCAR AS PERGUNTAS CADASTRADAS NO BANCO DE DADOS
            dbQuestions = new List<QuestionDb>();
            for (int i = 0; i < 1000; i++)
            {

                dbQuestions.Add(new QuestionDb
                {
                    QuestionId = i,
                    Question = $"Question number {i}",
                    Standards = $"Standard number {i}",
                    Requirements = $"Requirement number {i}",
                });
            }

            foreach (var question in dbQuestions)
            {
                var ans = answers.FirstOrDefault(a => a.QuestionId == question.QuestionId && a.CompanyId.Equals(companyId));

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
                    Requirement = question.Requirements,
                    Standard = question.Standards,
                    State = isInCompliance ? "Atende" : "Não atende",
                    CompanyId = companyId
                });
            }

            return questions;
        }
    }
}
