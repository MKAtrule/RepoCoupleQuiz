using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class AuthRepository : BaseRepository<User>, IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }


        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.User.
                FirstOrDefaultAsync(us => us.GlobalId == id);
        }
        public async Task SaveRefreshTokenAsync(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
        public async Task SaveResetPasswordOtpAsync(User user, string otp)
        {
            user.ResetPasswordOtp = otp;
            user.ResetPasswordOtpExpiryTime = DateTime.UtcNow.AddMinutes(2);
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByOtpAsync(string email, string otp)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email && u.ResetPasswordOtp == otp && u.ResetPasswordOtpExpiryTime > DateTime.UtcNow);
        }
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(us => us.Email == email);
        }
        public async Task<User> UpdatePassword(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(us => us.Email == email);
            if (user != null)
            {
                user.Password = password;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return user;

            }
            else
            {
                throw new Exception("User not found");
            }
        }
        public async Task<User> FindUserAsync(AuthRequestDTO request)
        {
            return await _context.User
                                 .FirstOrDefaultAsync(us => us.Email == request.Email && us.Password == request.Password);
        }

        public async Task<List<User>> GetUsersWhoNotAttemptedTodayQuestion(List<UserAnswers> userAnswers)
        {
            List<Guid> userIds = userAnswers.Select(us => us.GlobalId).ToList();

            var allUsers = await _context.User.ToListAsync();

            var filteredUsers = allUsers.Where(us => !userIds.Contains(us.GlobalId)).ToList();

            var partnerInvitations = await _context.PartnerInvitation.ToListAsync();

            var partnerUserIds = partnerInvitations
                .SelectMany(pi => new List<Guid?> { pi.SenderUserId, pi.RecieverUserId })
                .Where(id => id.HasValue) 
                .Select(id => id.Value)   
                .Distinct()               
                .ToList();

            var usersInPartnerInvitations = filteredUsers
                .Where(us => partnerUserIds.Contains(us.GlobalId))
                .ToList();

            return usersInPartnerInvitations;
        }
    }

}
