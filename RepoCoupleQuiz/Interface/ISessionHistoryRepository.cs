using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface ISessionHistoryRepository : IBaseRepository<SessionHistory>
    {
        Task<List<SessionHistory>> GetSessionHistoryByIdAsync(Guid userId);
        Task<SessionDetailsRequestDTO> GetSessionDetailsBySessionIdAsync(Guid sessionId, Guid userId);
        Task<bool> HasUserAttemptedAsync(Guid userId, Guid partnerInvitationId);
    }
}
