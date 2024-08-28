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

    }
}
