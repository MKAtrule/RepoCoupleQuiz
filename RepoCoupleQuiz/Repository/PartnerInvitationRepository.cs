using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class PartnerInvitationRepository:BaseRepository<PartnerInvitation>,IPartnerInvitationRepository
    {
        private readonly ApplicationDbContext _context;
        public PartnerInvitationRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public Task<bool> CodeValidation(Guid code)
        {
            var IsValid = _context.PartnerInvitation
                                  .FirstOrDefaultAsync(pi=>pi.InvitationCode==code && pi.IsCodeUsed==false);
            if(IsValid != null) 
                return Task.FromResult(true);
            
            else
                return Task.FromResult(false);        
        }
        public async Task<PartnerInvitation> GetInvitationDetails(Guid code)
        {
            return await _context.PartnerInvitation
                                .Include(su => su.SenderUser)
                                .Include(ru => ru.RecieverUser)
                                .FirstOrDefaultAsync(cd=>cd.InvitationCode==code);
        }
        public async Task<PartnerInvitation> GetByIdAsync(Guid id)
        {
            return await _context.PartnerInvitation
                                 .Include(su => su.SenderUser)
                                 .Include(ru => ru.RecieverUser)
                                 .FirstOrDefaultAsync(pi=>pi.GlobalId==id);
                
        }
    }
}
