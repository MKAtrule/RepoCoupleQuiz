using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository

    { 
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        public async Task<Question> GetById(Guid id)
        {
            return await _context.Question
                                 .Include(opt=>opt.QuestionOption)
                                 .FirstOrDefaultAsync(q=>q.GlobalId == id);    
        }

    }
}
