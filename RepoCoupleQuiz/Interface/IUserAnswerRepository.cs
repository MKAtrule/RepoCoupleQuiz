using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IUserAnswerRepository:IBaseRepository<UserAnswers>
    {
        Task<UserAnswers> GetAnswerByUserAndQuestion(Guid userId, Guid questionId);
        Task<UserAnswers> GetUserWhoAttemptedQuestion(Guid userAnswerId);
        Task<List<UserAnswers>> GetUserAnswersAsync(Guid userId,Guid sessionId);
    }
}
