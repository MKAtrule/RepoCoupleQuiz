using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class AuthRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
