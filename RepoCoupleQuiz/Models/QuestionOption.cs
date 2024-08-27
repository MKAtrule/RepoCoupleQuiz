using System.ComponentModel.DataAnnotations.Schema;

namespace RepoCoupleQuiz.Models
{
    public class QuestionOption : BaseClass
    {
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public string OptionText { get; set; }
        public int OptionOrder { get; set; }
        public bool IsCorrect { get; set; }
        public ICollection<UserAnswers> UserAnswersSelf { get; set; }
        public ICollection<UserAnswers> UserAnswersPartner { get; set; }
    }
    //public class QuestionOption:BaseClass
    //{
    //    [ForeignKey("Question")]
    //    public Guid QuestionId { get; set; }
    //    public Question Question { get; set; }
    //    public string OptionText { get; set; }
    //    public int OptionOrder { get; set; }
    //    public bool IsCorrect { get; set; }
    //    public ICollection<UserAnswers> UserAnswersSelf { get; set; }
    //    public ICollection<UserAnswers> UserAnswersPartner { get; set; }
    //}
}
