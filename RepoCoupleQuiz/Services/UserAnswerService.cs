using AutoMapper;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Services
{
    public class UserAnswersService
    {
        private readonly IUserAnswerRepository userAnswersRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IPartnerInvitationRepository partnerInvitationRepository;

        public UserAnswersService(
            IUserAnswerRepository userAnswersRepository,
            IQuestionRepository questionRepository,
            IPartnerInvitationRepository partnerInvitationRepository
           )
        {
            this.userAnswersRepository = userAnswersRepository;
            this.questionRepository = questionRepository;
            this.partnerInvitationRepository = partnerInvitationRepository;
        }

        public async Task AddUserAnswersAsync(UserAnswersRequestDTO request)
        {
            var session = await partnerInvitationRepository.GetByIdAsync(request.PartnerInvitationId);
            if (session == null || session.IsCodeUsed == false)
            {
                throw new Exception("Invalid session or session code not used.");
            }

            //var questions = await questionRepository.GetQuestionsForSession(session.GlobalId);

            foreach (var answer in request.Answers)
            {
                var userAnswer = new UserAnswers
                {
                    UserId = request.UserId,
                    QuestionId = answer.QuestionId,
                    AnswerForself = answer.AnswerForSelfOptionId,
                    AnswerForPartner = answer.AnswerForPartnerOptionId,
                    AnswerDate = DateTime.UtcNow,
                    PartnerInvitationId = session.GlobalId
                };

                await userAnswersRepository.Create(userAnswer);
            }
        }
    }

}
