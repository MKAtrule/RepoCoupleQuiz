using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role> GetById(Guid id)
        {
            return await _context.Role
                                 .FirstOrDefaultAsync(r => r.GlobalId == id);
        }

        public async Task<Role> GetByName()
        {
            return await _context.Role
                                .FirstOrDefaultAsync(r => r.RoleName == "User");
        }
    }

}
