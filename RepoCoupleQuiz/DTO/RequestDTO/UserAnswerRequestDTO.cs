namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class UserAnswersRequestDTO
    {
        // user Id Submitting the answers
        public Guid UserId { get; set; }

        public Guid PartnerInvitationId { get; set; }

        public List<UserAnswerDTO> Answers { get; set; }
    }
}
