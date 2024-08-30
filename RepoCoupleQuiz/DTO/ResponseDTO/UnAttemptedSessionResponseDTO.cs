namespace RepoCoupleQuiz.DTO.ResponseDTO
{
    public class UnAttemptedSessionResponseDTO
    {
        public Guid SessionId { get; set; }
        public List<UnAttemptedQuestionDTO> UnAttemptedQuestionDTOs { get; set; }

    }
}
