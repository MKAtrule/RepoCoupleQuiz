using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
   

        public interface IRoleRepository : IBaseRepository<Role>
        {
            Task<Role> GetById(Guid id);
            Task<Role> GetByName();

        }
    
}
