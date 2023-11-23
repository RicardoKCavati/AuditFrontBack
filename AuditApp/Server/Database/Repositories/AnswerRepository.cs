using AuditApp.Shared.Models;
using AuditApp.Shared.Models.Repositories;

namespace AuditApp.Server.Database.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly AuditContext _auditContext;

        public AnswerRepository(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public void AddAnswer(AnswerModel answer)
        {
            _auditContext.Add(answer);
            _auditContext.SaveChanges();
        }

        public AnswerModel GetAnswer(string companyId, int questionId)
        {
            return _auditContext.Answers.FirstOrDefault(ans => ans.QuestionId == questionId && ans.CompanyId.Equals(companyId));
        }

        public List<AnswerModel> SelectAll()
        {
            return _auditContext.Answers.ToList();
        }

        public List<AnswerModel> SelectAllByCompanyId(string companyId)
        {
            return _auditContext.Answers.Where(answer => answer.CompanyId.Equals(companyId)).ToList();
        }

        public void UpdateAnswer(AnswerModel answer)
        {
            _auditContext.Update(answer);
            _auditContext.SaveChanges();
        }
    }
}
