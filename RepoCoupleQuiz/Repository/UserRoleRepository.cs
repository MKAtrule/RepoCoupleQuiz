using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<string>> GetUserRolesAsync(Guid userId)
        {
            return await _context.UserRole
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.RoleName)
                .ToListAsync();
        }
    }
}
