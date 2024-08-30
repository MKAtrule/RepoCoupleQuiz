namespace RepoCoupleQuiz.DTO.ResponseDTO
{
    public class PartnerInvitationResponseDTO
    { 
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderImage { get; set; }
        public  Guid ?RecieverId  { get; set; }
        public string RecieverName { get; set; }
        public string RecieverImage { get; set; }

    }
}
