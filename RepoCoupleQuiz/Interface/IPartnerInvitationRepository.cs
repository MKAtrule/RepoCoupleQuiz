using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IPartnerInvitationRepository:IBaseRepository<PartnerInvitation>
    {
        Task<bool> CodeValidation(Guid code);
        Task<PartnerInvitation> GetInvitationDetails(Guid code);
        Task<PartnerInvitation> GetByIdAsync(Guid id);
        Task<List<PartnerInvitation>> GetAll();
    }
}
