namespace AuditApp.Shared.Models.Repositories
{
    public interface IQuestionRepository
    {
        List<QuestionDb> GetAllQuestions();
        int GetQuestionCount();
    }
}
