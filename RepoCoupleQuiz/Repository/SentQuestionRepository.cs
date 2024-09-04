using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Repository
{
    public class SentQuestionRepository : BaseRepository<SentQuestion>, ISentQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public SentQuestionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task MarkAsSentAsync(Guid questionId,DateTime today)
        {
            var sentQuestion = new SentQuestion
            {
                QuestionId = questionId,
                SentDate = today
            };
            await _context.SentQuestion.AddAsync(sentQuestion);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Guid>> GetSentQuestionIdsAsync()
        {
            return await _context.SentQuestion
                                 .Select(sq => sq.QuestionId)
                                 .ToListAsync();
        }
        public async Task<List<SentQuestion>> GetSentQuestionsByDateAsync(DateTime date)
        {
            return await _context.SentQuestion
                                 .Where(sq => sq.SentDate.Date == date)
                                 .ToListAsync();
        }

        public async Task<SentQuestion> GetSentQuestionsByPreviousDateAsync()
        {
            var yesterday= DateTime.Today.AddDays(-1);
            yesterday = yesterday.Date;
            return await _context.SentQuestion
                                 .FirstOrDefaultAsync(sq => sq.SentDate.Date == yesterday);
        }
    }
}
