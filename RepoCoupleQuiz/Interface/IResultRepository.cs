using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IResultRepository : IBaseRepository<Result>
    {
        Task<List<Result>> GetAllResultsByPartnerInvitation(Guid id);
    }

}
