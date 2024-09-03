using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class OptionCreateRequestDTO
    {
        [Required]
        public string Text { get; set; }

    }
}
