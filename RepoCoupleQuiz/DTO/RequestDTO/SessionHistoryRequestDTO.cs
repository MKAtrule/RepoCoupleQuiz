using System.ComponentModel.DataAnnotations;

namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class SessionHistoryRequestDTO
    {
        [Required]
        public Guid SessionId { get; set; }
        [Required]
        public UserAnswersRequestDTO UserAnswers { get; set; }  
    }
}
