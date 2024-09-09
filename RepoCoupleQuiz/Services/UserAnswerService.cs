using AutoMapper;
using Microsoft.Extensions.Configuration.UserSecrets;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RepoCoupleQuiz.Services
{
    public class UserAnswersService
    {
        private readonly IUserAnswerRepository userAnswersRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IPartnerInvitationRepository partnerInvitationRepository;
        private readonly IResultRepository resultRepository;
        private readonly ISessionHistoryRepository sessionHistoryRepository;
        private readonly IMapper mapper;

        public UserAnswersService(
            IUserAnswerRepository userAnswersRepository,
            IQuestionRepository questionRepository,
            IPartnerInvitationRepository partnerInvitationRepository,
            IResultRepository resultRepository,
            ISessionHistoryRepository sessionHistoryRepository,
            IMapper mapper)
        {
            this.userAnswersRepository = userAnswersRepository;
            this.questionRepository = questionRepository;
            this.partnerInvitationRepository = partnerInvitationRepository;
            this.resultRepository = resultRepository;
            this.sessionHistoryRepository = sessionHistoryRepository;
            this.mapper = mapper;
        }
        public async Task<List<UserAnswerResultResponseDTO>> AddUserAnswersandResultAsync(UserAnswersRequestDTO request)
        {
            var session = await partnerInvitationRepository.GetByIdAsync(request.PartnerInvitationId);
            if (session == null || session.IsCodeUsed == false)
            {
                throw new Exception("Invalid sessions or session code not used.");
            }

            var resultResponses = new List<UserAnswerResultResponseDTO>();
            var hasUserAlreadyAttempted = await userAnswersRepository.CheckUserAnswerForQuestion(request.UserId, request.Answer.QuestionId, request.PartnerInvitationId);
            if (hasUserAlreadyAttempted != null)
            {
                throw new Exception("Already Attempted this Question");
            }
            var userAnswer = new UserAnswers
            {
                UserId = request.UserId,
                QuestionId = request.Answer.QuestionId,
                AnswerForself = request.Answer.AnswerForSelfOptionId,
                AnswerForPartner = request.Answer.AnswerForPartnerOptionId,
                AnswerDate = DateTime.UtcNow,
                PartnerInvitationId = session.GlobalId,
                Active = true,
                CreatedAt = DateTime.UtcNow,
            };
            var newUserAnswer = await userAnswersRepository.Create(userAnswer);

            var userAttempted =await userAnswersRepository.GetUserWhoAttemptedQuestion(newUserAnswer.GlobalId);
            var partnerId = Guid.NewGuid();
            if (userAttempted.PartnerInvitation.SenderUserId==request.UserId)
            {
                partnerId = (Guid)userAttempted.PartnerInvitation.RecieverUserId;
            }
            else
            {
                partnerId = userAttempted.PartnerInvitation.SenderUserId;
            }
             var hasPartnerAttempted = await userAnswersRepository.CheckUserAnswerForQuestion(partnerId, request.Answer.QuestionId, request.PartnerInvitationId);
            if (hasPartnerAttempted == null)
            {
                var sessionHistory = new SessionHistory
                {

                    UserId = partnerId,
                    PartnerUserId = request.UserId,
                    QuestionId = request.Answer.QuestionId,
                    PartnerInvitationId = session.GlobalId,
                    IsAttempted = false,
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                };

                await sessionHistoryRepository.Create(sessionHistory);
                throw new Exception("Your Partner Not Attempted yet");
            }
            else
            {
                var partnerAnswers = new UserAnswers
                {
                    UserId = hasPartnerAttempted.UserId,
                    QuestionId = hasPartnerAttempted.QuestionId,
                    PartnerInvitationId = hasPartnerAttempted.PartnerInvitationId,
                    AnswerForself = hasPartnerAttempted.AnswerForself,
                    AnswerForPartner = hasPartnerAttempted.AnswerForPartner,
                };

                var userAnswers = new List<UserAnswers>();
                userAnswers.Add(newUserAnswer);
                userAnswers.Add(partnerAnswers);
                foreach (var firstAnswer in userAnswers)
                {
                    var result = new Result();

                    foreach (var secondAnswer in userAnswers)
                    {
                        result.UserId = firstAnswer.UserId;
                        if (firstAnswer == secondAnswer) continue;
                        result.PartnerUserId=secondAnswer.UserId;
                        if (firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
                        {
                            result.IsAnswerCorrectAboutPartner = true;
                            result.UserScore = 10;

                            if (firstAnswer.AnswerForself == secondAnswer.AnswerForPartner)
                            {
                                result.PartnerScore = 10;
                            }
                            else
                            {
                                result.PartnerScore = 0;
                            }
                        }
                        else
                        {
                            result.IsAnswerCorrectAboutPartner = false;
                            result.UserScore = 0;
                        }

                        if (firstAnswer.AnswerForself == secondAnswer.AnswerForPartner &&
                            firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
                        {
                            result.UserScore = 10;
                            result.PartnerScore = 10;
                            result.IsAnswerCorrectAboutPartner = true;
                            result.IsBothMatch = true;
                            break;
                        }
                    }
                    result.QuestionId = firstAnswer.QuestionId;
                    result.PartnerInvitationId = newUserAnswer.PartnerInvitationId;

                    var newResult = await resultRepository.Create(result);
                    var resultResponse = mapper.Map<UserAnswerResultResponseDTO>(newResult);
                    resultResponses.Add(resultResponse);
                }


                return resultResponses;

            }

        }



    }

}















    

