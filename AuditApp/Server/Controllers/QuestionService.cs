using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Repositories;

namespace AuditApp.Server.Controllers
{
    public class QuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public List<QuestionDb> GetAllQuestionsFromDatabase()
        {
            return _questionRepository.GetAllQuestions();
        }

        public int GetAllQuestionsCount()
        {
            return _questionRepository.GetQuestionCount();
        }
    }
}
