using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class AuthCreateRequestDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public IFormFile ProfileImage { get; set; }
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
