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










        // public async Task<List<UserAnswerResultResponseDTO>> AddUserAnswersandResultAsync(UserAnswersRequestDTO request)
        // {
        //     var session = await partnerInvitationRepository.GetByIdAsync(request.PartnerInvitationId);
        //     if (session == null || session.IsCodeUsed == false)
        //     {
        //         throw new Exception("Invalid sessions or session code not used.");
        //     }

        //     var resultResponses = new List<UserAnswerResultResponseDTO>();
        //     var userAnswer= new UserAnswers
        //     {
        //         UserId = request.UserId,
        //         QuestionId = request.Answer.QuestionId,
        //         AnswerForself = request.Answer.AnswerForSelfOptionId,
        //         AnswerForPartner = request.Answer.AnswerForPartnerOptionId,
        //         AnswerDate = DateTime.UtcNow,
        //         PartnerInvitationId = session.GlobalId
        //     };
        //     var userAnswers = request.Answers.Select(answer => new UserAnswers
        //     {
        //         UserId = request.UserId,
        //         QuestionId = answer.QuestionId,
        //         AnswerForself = answer.AnswerForSelfOptionId,
        //         AnswerForPartner = answer.AnswerForPartnerOptionId,
        //         AnswerDate = DateTime.UtcNow,
        //         PartnerInvitationId = session.GlobalId
        //     }).ToList();
        //     //Check if user already attempted this question or not
        //     var hasUserAlreadyAttempted = await userAnswersRepository.CheckUserAnswerForQuestion();
        ////     var newUserAnswer = await userAnswersRepository.Create(userAnswer);
        //     foreach (var userAnswer in userAnswers)
        //         {
        //           var newUserAnswer=  await userAnswersRepository.Create(userAnswer);
        //             if (userAnswers.Count == 1)
        //             {
        //           // ab result pr aik he error h sahi partneruserId bhjne h aur session history wala feature bhi add
        //           //krna h mtlb ye sessionid bhi lay request m 
        //             var userAttempted =await userAnswersRepository.GetUserWhoAttemptedQuestion(newUserAnswer.GlobalId);
        //             var userId = Guid.NewGuid();
        //             var partnerUserId=Guid.NewGuid();                   
        //             if(userAttempted.PartnerInvitation.SenderUserId == userAnswer.UserId)
        //              {
        //                     userId=(Guid)userAttempted.PartnerInvitation.RecieverUserId;
        //                     partnerUserId =userAttempted.PartnerInvitation.SenderUserId;
        //              }
        //             else
        //             {
        //                 userId=userAttempted.PartnerInvitation.SenderUserId ;
        //                 partnerUserId=(Guid)userAttempted.PartnerInvitation.RecieverUserId ;




        //             }
        //             var hasPartnerAttempt =await sessionHistoryRepository.HasUserAttemptedAsync(partnerUserId,userAnswer.QuestionId,userAnswer.PartnerInvitationId);
        //             if (hasPartnerAttempt == null)
        //             {
        //                 var sessionHistory = new SessionHistory
        //                 {

        //                     UserId = userId,
        //                     PartnerUserId = partnerUserId,
        //                     QuestionId = userAnswer.QuestionId,
        //                     PartnerInvitationId = session.GlobalId,
        //                     IsAttempted = false,
        //                     Active = true
        //                 };

        //                 await sessionHistoryRepository.Create(sessionHistory);
        //                 throw new Exception("Your Partner Not Attempted yet");
        //             }
        //             else
        //             {
        //                 var getUserSessionDetails = await sessionHistoryRepository.GetSessionDetailsBySessionIdAsync(partnerUserId,hasPartnerAttempt.GlobalId);
        //                 foreach (var firstAnswer in userAnswers)
        //                 {
        //                     var result = new Result();

        //                     foreach (var secondAnswer in getUserSessionDetails.Question.UserAnswer)
        //                     {
        //                         if (firstAnswer == secondAnswer) continue;

        //                         if (firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
        //                         {
        //                             result.IsAnswerCorrectAboutPartner = true;
        //                             result.UserScore = 10;

        //                             if (firstAnswer.AnswerForself == secondAnswer.AnswerForPartner)
        //                             {
        //                                 result.PartnerScore = 10;
        //                             }
        //                             else
        //                             {
        //                                 result.PartnerScore = 0;
        //                             }
        //                         }
        //                         else
        //                         {
        //                             result.IsAnswerCorrectAboutPartner = false;
        //                             result.UserScore = 0;
        //                         }

        //                         if (firstAnswer.AnswerForself == secondAnswer.AnswerForPartner &&
        //                             firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
        //                         {
        //                             result.UserScore = 10;
        //                             result.PartnerScore = 10;
        //                             result.IsAnswerCorrectAboutPartner = true;
        //                             result.IsBothMatch = true;
        //                             break;
        //                         }
        //                     }
        //                     result.UserId = request.UserId;
        //                     result.PartnerUserId = (Guid)session.RecieverUserId;
        //                     result.QuestionId = firstAnswer.QuestionId;
        //                     result.PartnerInvitationId = session.GlobalId;

        //                     var newResult = await resultRepository.Create(result);
        //                     var resultResponse = mapper.Map<UserAnswerResultResponseDTO>(newResult);
        //                     resultResponses.Add(resultResponse);
        //                 }
        //                 return resultResponses;

        //             }
        //         }
        //         else
        //         {
        //             var sessionHistory = new SessionHistory
        //             {
        //                 UserId = request.UserId,
        //                 PartnerUserId = (Guid)session.RecieverUserId,
        //                 QuestionId = userAnswer.QuestionId,
        //                 PartnerInvitationId = session.GlobalId,
        //                 IsAttempted = true,
        //                 Active = false
        //             };
        //             await sessionHistoryRepository.Update(sessionHistory);
        //         }
        //     }


        //         foreach (var firstAnswer in userAnswers)
        //         {
        //             var result = new Result();

        //             foreach (var secondAnswer in userAnswers)
        //             {
        //                 if (firstAnswer == secondAnswer) continue;

        //                 if (firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
        //                 {
        //                     result.IsAnswerCorrectAboutPartner = true;
        //                     result.UserScore = 10;

        //                     if (firstAnswer.AnswerForself == secondAnswer.AnswerForPartner)
        //                     {
        //                         result.PartnerScore = 10;
        //                     }
        //                     else
        //                     {
        //                         result.PartnerScore = 0;
        //                     }
        //                 }
        //                 else
        //                 {
        //                     result.IsAnswerCorrectAboutPartner = false;
        //                     result.UserScore = 0;
        //                 }

        //                 if (firstAnswer.AnswerForself == secondAnswer.AnswerForPartner &&
        //                     firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
        //                 {
        //                     result.UserScore = 10;
        //                     result.PartnerScore = 10;
        //                     result.IsAnswerCorrectAboutPartner = true;
        //                     result.IsBothMatch = true;
        //                     break;
        //                 }
        //             }
        //             result.UserId = request.UserId;
        //             result.PartnerUserId = (Guid)session.RecieverUserId;
        //             result.QuestionId = firstAnswer.QuestionId;
        //             result.PartnerInvitationId = session.GlobalId;

        //             var newResult = await resultRepository.Create(result);
        //             var resultResponse = mapper.Map<UserAnswerResultResponseDTO>(newResult);
        //             resultResponses.Add(resultResponse);
        //         }


        //     return resultResponses;
        // }






    

