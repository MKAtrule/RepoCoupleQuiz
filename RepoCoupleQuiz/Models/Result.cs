using System.ComponentModel.DataAnnotations.Schema;

namespace RepoCoupleQuiz.Models
{
    public class Result : BaseClass
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("PartnerUser")]
        public Guid PartnerUserId { get; set; }
        public User PartnerUser { get; set; }

        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        [ForeignKey("PartnerInvitation")]
        public Guid PartnerInvitationId { get; set; }
        public PartnerInvitation PartnerInvitation { get; set; }
        public bool IsAnswerCorrectAboutPartner { get; set; }
        public bool IsBothMatch { get; set; }
        public int UserScore { get; set; }
        public int PartnerScore { get; set; }
        public DateTime ResultDate { get; set; } 

    }

}
