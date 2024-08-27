using RepoCoupleQuiz.DTO.RequestDTO;

namespace RepoCoupleQuiz.DTO.ResponseDTO
{
    public class QuestionResponseDTO
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public ICollection<OptionRequestDTO> Options { get; set; }
    }
}
