namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class QuestionResultDTO
    {
        
        public Guid QuestionId { get; set; }


        public string QuestionText { get; set; }

        public Guid SenderOptionId { get; set; }

        public Guid RecieverOptionId { get; set; }

        public bool IsMatch { get; set; }
    }

}
