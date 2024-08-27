using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.ResponseDTO
{
    public class AuthCreateResponseDTO
    {
        public Guid UserId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        public string ProfileImage { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; }
    }
}
