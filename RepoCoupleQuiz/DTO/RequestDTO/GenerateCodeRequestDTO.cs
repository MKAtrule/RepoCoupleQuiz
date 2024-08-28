using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class GenerateCodeRequestDTO
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
