using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IUserAnswerRepository:IBaseRepository<UserAnswers>
    {
        Task<UserAnswers> GetAnswerByUserAndQuestion(Guid userId, Guid questionId);
        Task<UserAnswers> GetUserWhoAttemptedQuestion(Guid userAnswerId);
        Task<List<UserAnswers>> GetUsersWhoAttemptedTodayQuestion(Guid questionId);
        Task<List<UserAnswers>> GetUserAnswersAsync(Guid userId,Guid sessionId);
        Task<UserAnswers> CheckUserAnswerForQuestion(Guid userId, Guid questionId,Guid partnerInvitationId);
    }
}
