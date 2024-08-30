﻿using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Interface
{
    public interface IUserAnswerRepository:IBaseRepository<UserAnswers>
    {
        Task<UserAnswers> GetAnswerByUserAndQuestion(Guid userId, Guid questionId);
    }
}
