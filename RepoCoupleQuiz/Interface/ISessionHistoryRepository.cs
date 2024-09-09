using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface ISessionHistoryRepository : IBaseRepository<SessionHistory>
    {
        Task<List<SessionHistory>> GetSessionHistoryByIdAsync(Guid userId);
        Task<SessionHistory> GetSessionDetailsBySessionIdAsync(Guid sessionId);  
        Task<SessionHistory> HasUserAttemptedAsync(Guid userId, Guid questionId, Guid partnerInvitationId);
        
    }
}
