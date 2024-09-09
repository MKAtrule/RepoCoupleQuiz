using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IAuthRepository : IBaseRepository<User>
    {
        Task<User> FindUserAsync(AuthRequestDTO request);
        Task<User> GetByIdAsync(Guid id);
        Task SaveRefreshTokenAsync(User user, string refreshToken);
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);
        Task SaveResetPasswordOtpAsync(User user, string otp);
        Task<User> GetUserByOtpAsync(string email, string otp);
        Task<User> FindByEmailAsync(string email);
        Task<User> UpdatePassword(string email, string password);
        Task<List<User>> GetUsersWhoNotAttemptedTodayQuestion(List<UserAnswers> users);
        Task<List<User>> GetUsersByIds(List<Guid> users);

    }
}
