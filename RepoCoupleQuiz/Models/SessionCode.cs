using System.ComponentModel.DataAnnotations.Schema;

namespace RepoCoupleQuiz.Models
{
    public class SessionCode:BaseClass
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid UniqueCode { get; set; }
        public ICollection<PartnerInvitation> PartnerInvitation { get; set; }
    }
}
