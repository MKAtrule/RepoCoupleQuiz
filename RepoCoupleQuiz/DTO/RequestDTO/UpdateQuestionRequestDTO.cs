using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class UpdateQuestionRequestDTO
    {
        [Required]
        public Guid QuestionId { get; set; }    
        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
       
        public ICollection<OptionRequestDTO> Options { get; set; }
    }
}
