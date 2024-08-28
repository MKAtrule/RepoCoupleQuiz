namespace RepoCoupleQuiz.Interface
{
    public interface ISentQuestionRepository
    {
        Task MarkAsSentAsync(Guid questionId);
        Task<List<Guid>> GetSentQuestionIdsAsync();
    }
}
