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
        public async Task<SessionHistory> GetSessionDetailsBySessionIdAsync(Guid sessionId)
        {
            return await _context.SessionHistory
                                 .Include(us => us.User)
                                 .Include(pt => pt.PartnerUser)
                                 .Include(q => q.Question)
                                 .FirstOrDefaultAsync(sh=>sh.GlobalId==sessionId 
                                                      &&!sh.IsAttempted
                                                      &&sh.Active);
        }
        public async Task<List<SessionHistory>> GetSessionHistoryByIdAsync(Guid userId)
        {
            return await _context.SessionHistory
                                 .Include(us => us.User)
                                 .Include(pi => pi.PartnerInvitation)
                                 .Include(pt => pt.PartnerUser)
                                 .Include(q => q.Question)
                                 .ThenInclude(qo=>qo.QuestionOption)
                                 .Where(us => us.UserId == userId 
                                        && !us.IsAttempted
                                        &&us.Active)
                                 .ToListAsync();
        }
        public async Task<SessionHistory> HasUserAttemptedAsync(Guid userId,Guid questionId, Guid partnerInvitationId)
        {
            return await _context.SessionHistory
                .FirstOrDefaultAsync(sh => sh.PartnerUserId == userId
                                && sh.PartnerInvitationId == partnerInvitationId
                                && sh.QuestionId == questionId
                                && sh.IsAttempted);
        }


    }
}
