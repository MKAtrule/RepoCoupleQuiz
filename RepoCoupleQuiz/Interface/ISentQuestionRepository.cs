using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface ISentQuestionRepository
    {
        Task MarkAsSentAsync(Guid questionId,DateTime date);
        Task<List<Guid>> GetSentQuestionIdsAsync();
        Task<List<SentQuestion>> GetSentQuestionsByDateAsync(DateTime date);
        Task<SentQuestion> GetSentQuestionsByPreviousDateAsync();

    }
}
