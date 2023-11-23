using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Repositories;

namespace AuditApp.Server.Database.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AuditContext _auditContext;

        public QuestionRepository(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public List<QuestionDb> GetAllQuestions()
        {
            return _auditContext.Questions.ToList();
        }

        public int GetQuestionCount()
        {
            return _auditContext.Questions.Count();
        }
    }
}
