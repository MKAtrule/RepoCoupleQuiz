using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface ISessionHistoryRepository : IBaseRepository<SessionHistory>
    {
        Task<List<SessionHistory>> GetSessionHistoryByIdAsync(Guid userId);
        Task<SessionDetailsRequestDTO> GetSessionDetailsBySessionIdAsync(Guid sessionId, Guid userId);
        //Task<SessionHistory> GetSessionsByUserIdAsync();
        //Task<List<SessionHistory>> GetUnattemptedSessionsAsync(Guid userId);
    }
}
