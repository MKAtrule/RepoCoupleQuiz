namespace RepoCoupleQuiz.DTO.RequestDTO
{
    public class UpdateUserRequestDTO
    {

        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public IFormFile Image { get; set; }
    }
}
