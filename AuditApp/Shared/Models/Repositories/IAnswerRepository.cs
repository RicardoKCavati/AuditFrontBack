namespace AuditApp.Shared.Models.Repositories
{
    public interface IAnswerRepository
    {
        AnswerModel GetAnswer(string companyId, int questionId);
        List<AnswerModel> SelectAll();
        void UpdateAnswer(AnswerModel answer);
        void AddAnswer(AnswerModel answer);
    }
}
