using System.ComponentModel.DataAnnotations.Schema;

namespace RepoCoupleQuiz.Models
{
    public class SentQuestion:BaseClass
    {
        [ForeignKey("Question")]     
        public Guid QuestionId { get; set; }
        public DateTime SentDate { get; set; }
        public Question Question { get; set; }
    }

}
