using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class UserAnswerRepository:BaseRepository<UserAnswers>,IUserAnswerRepository
    {
        private readonly ApplicationDbContext _context;
        public UserAnswerRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task<UserAnswers> GetAnswerByUserAndQuestion(Guid userId, Guid questionId)
        {
            return await _context.UserAnswer
                .FirstOrDefaultAsync(ua => ua.UserId == userId
                                           && ua.QuestionId == questionId
                                           ); 
        }
    }
}
