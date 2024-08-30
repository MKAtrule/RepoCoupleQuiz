using AutoMapper;
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

            var userAnswers = request.Answers.Select(answer => new UserAnswers
            {
                UserId = request.UserId,
                QuestionId = answer.QuestionId,
                AnswerForself = answer.AnswerForSelfOptionId,
                AnswerForPartner = answer.AnswerForPartnerOptionId,
                AnswerDate = DateTime.UtcNow,
                PartnerInvitationId = session.GlobalId
            }).ToList();
            
            foreach (var userAnswer in userAnswers)
                {
                    await userAnswersRepository.Create(userAnswer);
                    if (userAnswers.Count == 1)
                    {
                    //abhi kiya krna h k is m jo jisna attempt nhi kiya usy assign krna h
                    var sessionHistory = new SessionHistory
                    {
                        UserId = userAnswer.UserId,
                        PartnerUserId = (Guid)session.RecieverUserId,
                        QuestionId = userAnswer.QuestionId,
                        PartnerInvitationId = session.GlobalId,
                        IsAttempted = false,
                        Active = true
                    };

                    await sessionHistoryRepository.Create(sessionHistory);
                    throw new Exception("Your Partner Not Attempted yet");
                    break;
                 }
                else
                {
                    var sessionHistory = new SessionHistory
                    {
                        UserId = request.UserId,
                        PartnerUserId = (Guid)session.RecieverUserId,
                        QuestionId = userAnswer.QuestionId,
                        PartnerInvitationId = session.GlobalId,
                        IsAttempted = true,
                        Active = false
                    };
                    await sessionHistoryRepository.Update(sessionHistory);
                }
            }


                foreach (var firstAnswer in userAnswers)
                {
                    var result = new Result();

                    foreach (var secondAnswer in userAnswers)
                    {
                        if (firstAnswer == secondAnswer) continue;

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
                    //result.User.Name =firstAnswer.User.Name;
                    //result.PartnerUser.Name = firstAnswer.User.Name;
                    result.UserId = request.UserId;
                    result.PartnerUserId = (Guid)session.RecieverUserId;
                    result.QuestionId = firstAnswer.QuestionId;
                    result.PartnerInvitationId = session.GlobalId;

                    var newResult = await resultRepository.Create(result);
                //var resultRespo = new UserAnswerResultResponseDTO()
                //{
                //    UserName=newResult.User.Name,
                //    PartnerUserName=newResult.PartnerUser.Name,
                //    IsAnswerCorrectAboutPartner=newResult.IsAnswerCorrectAboutPartner,
                //    UserScore=newResult.UserScore,
                //    PartnerScore=newResult.PartnerScore,

                //};
                   var resultResponse = mapper.Map<UserAnswerResultResponseDTO>(newResult);
                    resultResponses.Add(resultResponse);
                }
            
            
            return resultResponses;
        }

        //public async Task<List<UserAnswerResultResponseDTO>> AddUserAnswersandResultAsync(UserAnswersRequestDTO request)
        //{
        //    var session = await partnerInvitationRepository.GetByIdAsync(request.PartnerInvitationId);
        //    if (session == null || session.IsCodeUsed == false)
        //    {
        //        throw new Exception("Invalid sessions or session code not used.");
        //    }

        //    var resultResponses = new List<UserAnswerResultResponseDTO>();

        //    var userAnswers = request.Answers.Select(answer => new UserAnswers
        //    {
        //        UserId = request.UserId,
        //        QuestionId = answer.QuestionId,
        //        AnswerForself = answer.AnswerForSelfOptionId,
        //        AnswerForPartner = answer.AnswerForPartnerOptionId,
        //        AnswerDate = DateTime.UtcNow,
        //        PartnerInvitationId = session.GlobalId
        //    }).ToList();

        //    foreach (var userAnswer in userAnswers)
        //    {
        //        await userAnswersRepository.Create(userAnswer);
        //    }

        //    foreach (var firstAnswer in userAnswers)
        //    {
        //        var result = new Result();
        //        bool isMatch = false;

        //        foreach (var secondAnswer in userAnswers)
        //        {
        //            if (firstAnswer == secondAnswer) continue;
        //            if (firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
        //            {
        //                result.IsAnswerCorrectAboutPartner = true;
        //                result.UserScore = 10;
        //                if(firstAnswer.AnswerForself==secondAnswer.AnswerForPartner)
        //                {
        //                    result.PartnerScore = 10;
        //                }
        //                else
        //                {
        //                    result.PartnerScore = 0;
        //                }
        //            }
        //            else
        //            {
        //               result.IsAnswerCorrectAboutPartner=false;
        //                result.UserScore = 0;

        //            }
        //            if (firstAnswer.AnswerForself == secondAnswer.AnswerForPartner &&
        //                firstAnswer.AnswerForPartner == secondAnswer.AnswerForself)
        //            {
        //                result.UserScore=10;
        //                result.PartnerScore = 10;
        //                result.IsAnswerCorrectAboutPartner = true;
        //                result.IsBothMatch = true;
        //                break;
        //                                  }
        //            }
        //        result.UserId = request.UserId;
        //        result.PartnerUserId = (Guid)session.RecieverUserId;
        //        result.QuestionId = firstAnswer.QuestionId;
        //        result.PartnerInvitationId = session.GlobalId;
        //      var newResult=  await resultRepository.Create(result);

        //        var resultResponse = mapper.Map<UserAnswerResultResponseDTO>(newResult);
        //        resultResponses.Add(resultResponse);
        //    }

        //    return resultResponses;
        //}

    }
}
