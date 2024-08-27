using System.ComponentModel.DataAnnotations.Schema;

namespace RepoCoupleQuiz.Models
{
    public class SessionHistory : BaseClass
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

        public bool IsAttempted { get; set; }
    }
    //public class SessionHistory:BaseClass
    //{
    //    [ForeignKey("User")]
    //    public Guid UserId { get; set; }
    //    public User User { get; set; }
    //    [ForeignKey("User")]
    //    public Guid PartnerUserId { get; set; }
    //    public User PartnerUser { get; set; }
    //    [ForeignKey("Question")]
    //    public Guid QuestionId { get; set; }
    //    public Question Question { get; set; }
    //    public bool IsAttempted { get; set; }

    //}
}
