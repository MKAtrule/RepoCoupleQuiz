using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        private readonly ApplicationDbContext _context;
        public ResultRepository(ApplicationDbContext context) : base(context) 
        { 
            _context = context;
        }
        public async Task<List<Result>> GetAllResultsByPartnerInvitation(Guid id)
        {
            return await _context.Result
                                 .Include(pi=>pi.PartnerInvitation)
                                 .Include(us=>us.User)
                                 .Include(q=>q.Question)
                                 .Include(pu=>pu.PartnerUser)
                                 .Where(r=>r.PartnerInvitationId == id)
                                 .ToListAsync();
        }
    }
}
