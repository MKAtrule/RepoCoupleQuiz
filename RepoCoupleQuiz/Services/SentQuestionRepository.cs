using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;
using RepoCoupleQuiz.Repository;

namespace RepoCoupleQuiz.Services
{
    public class SentQuestionRepository : BaseRepository<SentQuestion>,ISentQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public SentQuestionRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task MarkAsSentAsync(Guid questionId)
        {
            var sentQuestion = new SentQuestion
            {
                QuestionId = questionId,
                SentDate = DateTime.UtcNow
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
    }
}
