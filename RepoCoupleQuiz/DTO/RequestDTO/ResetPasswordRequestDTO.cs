using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class ResetPasswordRequestDTO
    {
        public Guid UserId { get; set; }
        [MaxLength(50)]
        public string NewPassword { get; set; }

    }
}
