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
        public SessionHistoryService(ISessionHistoryRepository sessionHistoryRepository, IResultRepository resultRepository)
        {
            this.sessionHistoryRepository = sessionHistoryRepository;
            this.resultRepository = resultRepository;
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
        //    public async Task<UserAnswerResultResponseDTO> s(Guid sessionId, Guid userId, UserAnswersRequestDTO answersRequest)
        //    {
        //        // Fetch session details to check unattempted questions
        //        var sessionDetails = await sessionHistoryRepository.GetSessionDetailsBySessionIdAsync(sessionId, userId);

        //        if (!sessionDetails.UnattemptedQuestions.Any())
        //        {
        //            throw new Exception("No unattempted questions found for this session.");
        //        }

        //        // Check if partner has attempted the session
        //        var partnerAttempted = sessionDetails.SessionHistory.Any(sh => sh.PartnerUserId == userId && sh.IsAttempted);

        //        if (!partnerAttempted)
        //        {
        //            // If the partner has not attempted, generate a response indicating that
        //            return new UserAnswerResultResponseDTO
        //            {
        //                UserName = sessionDetails.,
        //                PartnerUserName = sessionDetails.PartnerName,
        //                Message = "Your partner has not attempted the session yet.",
        //                UserScore = 0,
        //                PartnerScore = 0,
        //                AreBothAnswersMatching = false
        //            };
        //        }

        //        // If the partner has attempted, process the user answers
        //        var userAnswers = answersRequest.Answers.Select(answer => new UserAnswers
        //        {
        //            UserId = answersRequest.UserId,
        //            QuestionId = answer.QuestionId,
        //            AnswerForself = answer.AnswerForSelfOptionId,
        //            AnswerForPartner = answer.AnswerForPartnerOptionId,
        //            AnswerDate = DateTime.UtcNow,
        //            PartnerInvitationId = sessionDetails.PartnerInvitationId
        //        }).ToList();

        //        var userResults = new List<Result>();

        //        foreach (var firstAnswer in userAnswers)
        //        {
        //            var result = new Result();
        //            var matchingAnswer = sessionDetails.PartnerAnswers
        //                .FirstOrDefault(partnerAnswer => partnerAnswer.QuestionId == firstAnswer.QuestionId);

        //            if (matchingAnswer != null)
        //            {
        //                // Check matching logic based on answers
        //                if (firstAnswer.AnswerForself == matchingAnswer.AnswerForPartner &&
        //                    firstAnswer.AnswerForPartner == matchingAnswer.AnswerForself)
        //                {
        //                    // Both answers match
        //                    result.IsBothMatch = true;
        //                    result.UserScore = 10;
        //                    result.PartnerScore = 10;
        //                }
        //                else
        //                {
        //                    // Check if only one side matches
        //                    result.IsBothMatch = false;
        //                    result.UserScore = firstAnswer.AnswerForPartner == matchingAnswer.AnswerForself ? 10 : 0;
        //                    result.PartnerScore = matchingAnswer.AnswerForPartner == firstAnswer.AnswerForself ? 10 : 0;
        //                }
        //            }
        //            else
        //            {
        //                result.UserScore = 0;
        //                result.PartnerScore = 0;
        //                result.IsBothMatch = false;
        //            }

        //            result.IsAnswerCorrectAboutPartner = result.UserScore > 0;
        //            result.UserId = userId;
        //            result.PartnerUserId = matchingAnswer?.UserId ?? Guid.Empty;
        //            result.QuestionId = firstAnswer.QuestionId;
        //            result.PartnerInvitationId = sessionDetails.PartnerInvitationId;

        //            userResults.Add(result);

        //            // Persist the result in the database
        //            await resultRepository.Create(result);
        //        }

        //        // Aggregate results into the response DTO
        //        var finalResponse = new UserAnswerResultResponseDTO
        //        {
        //            UserName = sessionDetails.UserName,
        //            PartnerUserName = sessionDetails.PartnerName,
        //            UserScore = userResults.Sum(r => r.UserScore),
        //            PartnerScore = userResults.Sum(r => r.PartnerScore),
        //            AreBothAnswersMatching = userResults.All(r => r.IsBothMatch)
        //        };

        //        return finalResponse;
        //    }

        //}

    }
}
