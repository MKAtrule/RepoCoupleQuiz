namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class UserAnswerDTO
    {
        public Guid QuestionId { get; set; }

        public Guid AnswerForSelfOptionId { get; set; }
        public Guid AnswerForPartnerOptionId { get; set; }
    }
}
