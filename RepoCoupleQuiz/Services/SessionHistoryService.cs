using AutoMapper;
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
        private readonly IMapper mapper;
        public SessionHistoryService(ISessionHistoryRepository sessionHistoryRepository, IResultRepository resultRepository, IUserAnswerRepository userAnswerRepository, IPartnerInvitationRepository partnerInvitationRepository, IMapper mapper)
        {
            this.sessionHistoryRepository = sessionHistoryRepository;
            this.resultRepository = resultRepository;
            this.userAnswersRepository = userAnswerRepository;
            this.partnerInvitationRepository = partnerInvitationRepository;
            this.mapper = mapper;
        }
        public async Task<List<UnAttemptedSessionResponseDTO>> GetUnAttemptedSessionsForUser(Guid userId)
        {
            var unattemptedSessions = await sessionHistoryRepository.GetSessionHistoryByIdAsync(userId);

            var unattemptedSessionDTOs = unattemptedSessions.Select(sh => new UnAttemptedSessionResponseDTO
            {
                SessionId = sh.PartnerInvitationId,
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

            return unattemptedSessionDTOs;
        }
        //public async Task<List<UserAnswerResultResponseDTO>> HandleUserAnswersAsync(Guid userId, Guid sessionId, UserAnswersRequestDTO request)
        //{
        //    var hasUserAttempted = await sessionHistoryRepository.HasUserAttemptedAsync(userId, sessionId);
        //    if (hasUserAttempted)
        //    {
        //        throw new Exception("You have already attempted this session.");
        //    }

        //    var session = await partnerInvitationRepository.GetByIdAsync(sessionId);
        //    if (session == null || !session.IsCodeUsed)
        //    {
        //        throw new Exception("Invalid session or session code not used.");
        //    }

        //    // checkinh if already attempt by pratner
        //    var partnerUserId = session.SenderUserId == userId ? session.RecieverUserId : session.SenderUserId;
        //    var hasPartnerAttempted = await sessionHistoryRepository.HasUserAttemptedAsync((Guid)partnerUserId, sessionId);

        //    // Add the user's answers and session history
        //    var userAnswers = request.Answers.Select(answer => new UserAnswers
        //    {
        //        UserId = userId,
        //        QuestionId = answer.QuestionId,
        //        AnswerForself = answer.AnswerForSelfOptionId,
        //        AnswerForPartner = answer.AnswerForPartnerOptionId,
        //        AnswerDate = DateTime.UtcNow,
        //        PartnerInvitationId = sessionId
        //    }).ToList();

        //    foreach (var userAnswer in userAnswers)
        //    {
        //        await userAnswersRepository.Create(userAnswer);
        //    }

        //    // Add the session history entry for the user
        //    var sessionHistory = new SessionHistory
        //    {
        //        UserId = userId,
        //        PartnerUserId = (Guid)partnerUserId,
        //        QuestionId = userAnswers.First().QuestionId, 
        //        PartnerInvitationId = sessionId,
        //        IsAttempted = true,
        //        Active = false,
        //        AttemptedDate = DateTime.UtcNow
        //    };
        //    await sessionHistoryRepository.Update(sessionHistory);

           
        //    if (!hasPartnerAttempted)
        //    {
        //        var partnerSessionHistory = new SessionHistory
        //        {
        //            UserId = (Guid)partnerUserId,
        //            PartnerUserId = userId,
        //            QuestionId = userAnswers.First().QuestionId,
        //            PartnerInvitationId = sessionId,
        //            IsAttempted = false,
        //            Active = true,
        //            AttemptedDate = DateTime.UtcNow
        //        };
        //        await sessionHistoryRepository.Create(partnerSessionHistory);
        //        throw new Exception("Your partner has not attempted the question yet.");
        //    }

        //    return await GenerateUserResults(userAnswers, userId, (Guid)partnerUserId, sessionId);
        //}

        //private async Task<List<UserAnswerResultResponseDTO>> GenerateUserResults(
        //    List<UserAnswers> userAnswers, Guid userId, Guid partnerUserId, Guid sessionId)
        //{
        //    var resultResponses = new List<UserAnswerResultResponseDTO>();

            
        //    var partnerAnswers = await userAnswersRepository.GetUserAnswersAsync(partnerUserId, sessionId);

        //    foreach (var userAnswer in userAnswers)
        //    {
        //        var correspondingPartnerAnswer = partnerAnswers.FirstOrDefault(pa => pa.QuestionId == userAnswer.QuestionId);
        //        if (correspondingPartnerAnswer == null) continue;

        //        var result = new Result
        //        {
        //            UserId = userId,
        //            PartnerUserId = partnerUserId,
        //            QuestionId = userAnswer.QuestionId,
        //            PartnerInvitationId = sessionId,
        //            IsAnswerCorrectAboutPartner = userAnswer.AnswerForPartner == correspondingPartnerAnswer.AnswerForself,
        //            UserScore = userAnswer.AnswerForPartner == correspondingPartnerAnswer.AnswerForself ? 10 : 0,
        //            PartnerScore = correspondingPartnerAnswer.AnswerForPartner == userAnswer.AnswerForself ? 10 : 0,
        //            IsBothMatch = userAnswer.AnswerForPartner == correspondingPartnerAnswer.AnswerForself
        //                           && userAnswer.AnswerForself == correspondingPartnerAnswer.AnswerForPartner
        //        };

        //        var newResult = await resultRepository.Create(result);
        //        var resultResponse = mapper.Map<UserAnswerResultResponseDTO>(newResult);
        //        resultResponses.Add(resultResponse);
        //    }

        //    return resultResponses;
        //}

    }

}

