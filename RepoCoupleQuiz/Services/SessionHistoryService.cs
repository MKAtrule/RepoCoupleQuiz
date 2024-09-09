using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;
using RepoCoupleQuiz.Repository;

namespace RepoCoupleQuiz.Services
{
    public class SessionHistoryService
    {
        private readonly ISessionHistoryRepository sessionHistoryRepository;
        private readonly IResultRepository resultRepository;
        private readonly IUserAnswerRepository userAnswersRepository;
        private readonly IPartnerInvitationRepository partnerInvitationRepository;
        private readonly ISentQuestionRepository sentQuestionRepository;
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;
        public SessionHistoryService(ISessionHistoryRepository sessionHistoryRepository, IResultRepository resultRepository, IUserAnswerRepository userAnswerRepository, IPartnerInvitationRepository partnerInvitationRepository, IMapper mapper, ISentQuestionRepository sentQuestionRepository, IAuthRepository authRepository)
        {
            this.sessionHistoryRepository = sessionHistoryRepository;
            this.resultRepository = resultRepository;
            this.userAnswersRepository = userAnswerRepository;
            this.partnerInvitationRepository = partnerInvitationRepository;
            this.mapper = mapper;
            this.sentQuestionRepository = sentQuestionRepository;
            this.authRepository = authRepository;
        }
        public async Task<List<UnAttemptedSessionResponseDTO>> GetUnAttemptedSessionsForUser(Guid userId)
        {
            var unattemptedSessions = await sessionHistoryRepository.GetSessionHistoryByIdAsync(userId);

            var unattemptedSession = unattemptedSessions.Select(sh => new UnAttemptedSessionResponseDTO
            {
                SessionId = sh.GlobalId,
                UnAttemptedQuestionDTOs = new List<UnAttemptedQuestionDTO>
                {
                      new UnAttemptedQuestionDTO
                 {
                QuestionId = sh.Question.GlobalId,
                QuestionText = sh.Question.QuestionText,
                Options = sh.Question.QuestionOption.Select(op => new OptionRequestDTO
                {
                    OptionId = op.GlobalId,
                    Text = op.OptionText,
                }).ToList(),
            }
                }
            }).ToList();

            return unattemptedSession;
        }
        public async Task<List<UserAnswerResultResponseDTO>> HandleSessionHistoryAnswer(SessionHistoryRequestDTO request)
        {
            var sessionDetails = await sessionHistoryRepository.GetSessionDetailsBySessionIdAsync(request.SessionId);
            if(sessionDetails == null)
            {
                throw new Exception("Invalid SessionId or UserId");
            }
       
            var userAnswer = new UserAnswers
            {
                UserId = request.UserAnswers.UserId,
                QuestionId = request.UserAnswers.Answer.QuestionId,
                AnswerForself = request.UserAnswers.Answer.AnswerForSelfOptionId,
                AnswerForPartner = request.UserAnswers.Answer.AnswerForPartnerOptionId,
                AnswerDate = DateTime.UtcNow,
                PartnerInvitationId = request.UserAnswers.PartnerInvitationId,
                Active = true,
                CreatedAt = DateTime.UtcNow,
            };
            var newUserAnswer = await userAnswersRepository.Create(userAnswer);
             sessionDetails.AttemptedDate = DateTime.UtcNow;
            sessionDetails.IsAttempted = true;
            sessionDetails.Active = false;
            await sessionHistoryRepository.Update(sessionDetails);
            var userAttempted = await userAnswersRepository.GetUserWhoAttemptedQuestion(newUserAnswer.GlobalId);
            var partnerId = Guid.NewGuid();
            if (userAttempted.PartnerInvitation.SenderUserId == request.UserAnswers.UserId)
            {
                partnerId = (Guid)userAttempted.PartnerInvitation.RecieverUserId;
            }
            else
            {
                partnerId = userAttempted.PartnerInvitation.SenderUserId;
            }
            var partnerAttemptedQuestion = await userAnswersRepository.CheckUserAnswerForQuestion(partnerId, request.UserAnswers.Answer.QuestionId, request.UserAnswers.PartnerInvitationId);
            if (partnerAttemptedQuestion is null)
            {
                throw new Exception("Your Partner has not attempted yet");
            }
            var partnerAnswers = new UserAnswers
            {
                UserId = partnerAttemptedQuestion.UserId,
                QuestionId = partnerAttemptedQuestion.QuestionId,
                PartnerInvitationId = partnerAttemptedQuestion.PartnerInvitationId,
                AnswerForself = partnerAttemptedQuestion.AnswerForself,
                AnswerForPartner = partnerAttemptedQuestion.AnswerForPartner,
            };
            var resultResponses = new List<UserAnswerResultResponseDTO>();
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
                    result.PartnerUserId = secondAnswer.UserId;
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
        public async Task AddQuestionToSessionHistory()
        {
            var allPartnerUsers = await partnerInvitationRepository.Get();

            var yesterdayQuestion = await sentQuestionRepository.GetSentQuestionsByPreviousDateAsync();

            if (yesterdayQuestion == null)
            {
                return;
            }

            var usersAttemptedQuestion = await userAnswersRepository.GetUsersWhoAttemptedTodayQuestion(yesterdayQuestion.QuestionId);

            var usersNotAttemptedQuestion = await authRepository.GetUsersWhoNotAttemptedTodayQuestion(usersAttemptedQuestion);

            foreach (var user in usersNotAttemptedQuestion)
            {
                var partnerInvitation = await GetPartnerInvitationDetails(allPartnerUsers, user.GlobalId);

                if (partnerInvitation == null)
                {
                    continue;
                }

                var sessionHistory = new SessionHistory
                {
                    UserId = user.GlobalId,
                    PartnerUserId = partnerInvitation.SenderUserId == user.GlobalId ? partnerInvitation.RecieverUserId.Value : partnerInvitation.SenderUserId,
                    PartnerInvitationId = partnerInvitation.GlobalId,
                    QuestionId = yesterdayQuestion.QuestionId,
                    IsAttempted = false
                };

                await sessionHistoryRepository.Create(sessionHistory);
            }

        }

        private async Task<PartnerInvitation> GetPartnerInvitationDetails(List<PartnerInvitation> partnerInvitations, Guid userId)
        {
            var partnerInvitation = partnerInvitations
                .FirstOrDefault(pi => pi.SenderUserId == userId || pi.RecieverUserId == userId);

            return await Task.FromResult(partnerInvitation);
        }

    }

}



