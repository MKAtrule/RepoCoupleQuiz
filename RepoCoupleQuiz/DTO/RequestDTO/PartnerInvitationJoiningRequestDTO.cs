using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class PartnerInvitationJoiningRequestDTO
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid Code { get; set; }
    }
}
