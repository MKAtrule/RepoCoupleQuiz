namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class UserAnswersRequestDTO
    {
        public Guid UserId { get; set; }

        public Guid PartnerInvitationId { get; set; }
        public UserAnswerDTO Answer { get; set; }
      //  public List<UserAnswerDTO> Answers { get; set; }
    }
}
