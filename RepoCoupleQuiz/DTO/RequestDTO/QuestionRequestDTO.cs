using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class QuestionRequestDTO
    {
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        public ICollection<OptionRequestDTO> Options { get; set; }
    }
}
