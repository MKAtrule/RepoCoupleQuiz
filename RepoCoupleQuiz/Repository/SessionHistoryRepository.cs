using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class SessionHistoryRepository : BaseRepository<SessionHistory>, ISessionHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public SessionHistoryRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<SessionDetailsRequestDTO> GetSessionDetailsBySessionIdAsync(Guid sessionId, Guid userId)
        {
            var sessionDetails = new SessionDetailsRequestDTO
            {
                SessionHistory = await _context.SessionHistory
                    .Where(sh => sh.PartnerInvitationId == sessionId && sh.Active)
                    .ToListAsync(),
                UserAnswer = await _context.UserAnswer
                    .Where(ua => ua.PartnerInvitationId == sessionId )
                    .ToListAsync(),
                Result = await _context.Result
                    .Where(r => r.PartnerInvitationId == sessionId )
                    .ToListAsync(),

                UnattemptedQuestions = await _context.Question
                    .Where(q => !_context.UserAnswer
                        .Any(ua => ua.UserId == userId && ua.QuestionId == q.GlobalId && ua.PartnerInvitationId == sessionId))
                    .ToListAsync()
            };
            return sessionDetails;
        }
        public async Task<List<SessionHistory>> GetSessionHistoryByIdAsync(Guid userId)
        {
            return await _context.SessionHistory
                                 .Include(us => us.User)
                                 .Include(pi => pi.PartnerInvitation)
                                 .Include(pt => pt.PartnerUser)
                                 .Include(q => q.Question)
                                 .ThenInclude(qo=>qo.QuestionOption)
                                 .Where(us => us.UserId == userId && !us.IsAttempted)
                                 .ToListAsync();
        }

    }
}
