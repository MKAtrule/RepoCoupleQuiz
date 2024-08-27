using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }

    }
}
