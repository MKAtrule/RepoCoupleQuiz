namespace RepoCoupleQuiz.Models
{
    public class User:BaseClass
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? ResetPasswordOtp { get; set; }
        public DateTime? ResetPasswordOtpExpiryTime { get; set; }
        public bool? IsOTpVerified { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
        public ICollection<UserAnswers> UserAnswer { get; set; }
        public ICollection<SessionHistory> SessionHistory { get; set; }
        public ICollection<PartnerInvitation> SentInvitation { get; set; }
        public ICollection<PartnerInvitation> ReceivedInvitation { get; set; }
        public ICollection<Result> Result { get; set; }
    //    public ICollection<SessionCode> SessionCode { get; set; }
    }
}
