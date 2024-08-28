namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class OptionRequestDTO
    {
        public Guid OptionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
