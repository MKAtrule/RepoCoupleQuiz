using RepoCoupleQuiz.DTO.RequestDTO;

namespace RepoCoupleQuiz.DTO.ResponseDTO
{
    public class UnAttemptedQuestionDTO
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public ICollection<OptionRequestDTO> Options { get; set; }
    }
}
