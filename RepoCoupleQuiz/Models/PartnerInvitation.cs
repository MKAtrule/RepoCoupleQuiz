using System.ComponentModel.DataAnnotations.Schema;

namespace RepoCoupleQuiz.Models
{
    public class PartnerInvitation : BaseClass
    {
        [ForeignKey("SenderUser")]
        public Guid SenderUserId { get; set; }
        public User SenderUser { get; set; }

        [ForeignKey("RecieverUser")]
        public Guid? RecieverUserId { get; set; }
        public User RecieverUser { get; set; }

        public Guid InvitationCode { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsCodeUsed { get; set; }
        public DateTime CodeExpires { get; set; }
        public ICollection<UserAnswers> UserAnswers { get; set; }
    }
}
