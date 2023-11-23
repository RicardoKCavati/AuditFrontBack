using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Dto;
using AuditApp.Shared.Models.Repositories;

namespace AuditApp.Server.Services
{
    public class AnswerService
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public List<AnswerModel> GetAnswersByCompanyId(string companyId)
        {
            return _answerRepository.SelectAllByCompanyId(companyId);
        }

        public void HandleAnswer(AnsweredQuestion answeredQuestion)
        {
            var answer = _answerRepository.GetAnswer(answeredQuestion.CompanyId, answeredQuestion.QuestionId);

            if (answer == null)
            {
                answer = new Shared.Models.AnswerModel()
                {
                    QuestionId = answeredQuestion.QuestionId,
                    CompanyId = answeredQuestion.CompanyId,
                    IsInCompliance = answeredQuestion.Complies
                };

                _answerRepository.AddAnswer(answer);
            }
            else
            {
                answer.IsInCompliance = answeredQuestion.Complies;
                _answerRepository.UpdateAnswer(answer);
            }
        }

        public List<AnswerModel> GetAllAnswers()
        {
            return _answerRepository.SelectAll();
        }
    }
}
