using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class QuestionOptionRepository:BaseRepository<QuestionOption>,IQuestionOptionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionOptionRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task AddOptions(List<QuestionOption> questionOptions)
        {
         await   _context.QuestionOption.AddRangeAsync(questionOptions);
          await  _context.SaveChangesAsync(); 
        }

        public async Task<List<QuestionOption>> GetOptionsByQuestionId(Guid id)
        {
            return await _context.QuestionOption
                                 .Where(x => x.QuestionId == id)
                                 .ToListAsync();

        }
        public async Task UpdateOptions(List<QuestionOption> options)
        {
             _context.QuestionOption.UpdateRange(options);
            await _context.SaveChangesAsync();
        }
        public async Task<QuestionOption> GetOptionById(Guid id)
        {
            return await _context.QuestionOption
                                 .FirstOrDefaultAsync(op=>op.GlobalId==id);
        }
    }
}
