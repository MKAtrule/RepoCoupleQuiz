﻿namespace RepoCoupleQuiz.DTO.ResponseDTO
{
    public class AuthResponseDTO
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
    }
}
