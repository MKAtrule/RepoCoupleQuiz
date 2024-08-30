namespace RepoCoupleQuiz.DTO.ResponseDTO
{
    public class UserAnswerResultResponseDTO
    {
        public string UserName { get; set; }
     //   public string ProfileImage { get; set; }
        public string PartnerUserName { get; set; }
        public bool IsAnswerCorrectAboutPartner { get; set; }
        public bool AreBothAnswersMatching { get; set; }
        public int UserScore { get; set; }
        public int PartnerScore { get; set; }
    }
}
