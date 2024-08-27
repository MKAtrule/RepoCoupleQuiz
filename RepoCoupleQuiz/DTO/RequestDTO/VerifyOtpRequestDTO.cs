using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class VerifyOtpRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Otp { get; set; }

    }
}
