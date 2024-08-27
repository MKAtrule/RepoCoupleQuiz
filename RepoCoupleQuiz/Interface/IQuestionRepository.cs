using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IQuestionRepository:IBaseRepository<Question>
    {
        Task<Question> GetById(Guid id);
    }
}
