using RepoCoupleQuiz.Models;
using System.Threading.Tasks;

namespace RepoCoupleQuiz.Interface
{
    public interface IQuestionOptionRepository : IBaseRepository<QuestionOption>
    {
        Task AddOptions(List<QuestionOption> questionOptions);
        Task<List<QuestionOption>> GetOptionsByQuestionId(Guid id);
        Task UpdateOptions(List<QuestionOption> options);
        Task<QuestionOption> GetOptionById(Guid id);
    }
}
