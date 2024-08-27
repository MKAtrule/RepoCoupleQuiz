using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task<List<string>> GetUserRolesAsync(Guid userId);
    }
}
