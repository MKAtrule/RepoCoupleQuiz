using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class SessionDetailsRequestDTO
    {
        public List<SessionHistory> SessionHistory { get; set; }
        public List<UserAnswers> UserAnswer { get; set; }
        public List<Result> Result { get; set; }
        public List<Question> UnattemptedQuestions { get; set; }
    }
}
