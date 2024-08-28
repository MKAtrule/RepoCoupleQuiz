namespace RepoCoupleQuiz.Models
{
    public class Question:BaseClass
    {
        public string QuestionText { get; set; }
        public ICollection<QuestionOption> QuestionOption { get; set; }
        public ICollection<UserAnswers> UserAnswer { get; set; }
        public ICollection<SessionHistory> SessionHistory { get; set; }
        public ICollection<Result> Result { get; set; }
        public ICollection<SentQuestion> SentQuestion { get; set; }
    }
}




















