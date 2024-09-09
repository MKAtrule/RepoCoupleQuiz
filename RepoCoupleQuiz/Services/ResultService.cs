using AutoMapper;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.DTO.ResponseDTO.RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Repository;

namespace RepoCoupleQuiz.Services
{
    public class ResultService
    {
        private readonly IResultRepository resultRepository;
        private readonly IMapper mapper;
        private readonly IAuthRepository authRepository;
        public ResultService(IResultRepository resultRepository, IMapper mapper, IAuthRepository authRepository)
        {
            this.resultRepository = resultRepository;
            this.mapper = mapper;
            this.authRepository = authRepository;
        }
        public async Task<List<UserAnswerResultResponseDTO>> GetAllResultByPartnerInvitationId(Guid id)
        {
            var allResults= await resultRepository.GetAllResultsByPartnerInvitation(id);
           if(allResults.Count == 0)
            {
                throw new Exception("No results for this partner Invitation id");
            }
           return    mapper.Map<List<UserAnswerResultResponseDTO>>(allResults);
        }
        public async Task<List<UserScoreResponseDTO>> CalculateScoresForPartnerInvitation(Guid partnerInvitationId)
        {
            var allResults = await resultRepository.GetAllResultsByPartnerInvitation(partnerInvitationId);
            if (allResults.Count == 0)
            {
                throw new Exception("No results for this partner invitation ID.");
            }

            var userScores = new Dictionary<Guid, int>();

            foreach (var result in allResults)
            {
                if (!userScores.ContainsKey(result.UserId))
                {
                    userScores[result.UserId] = 0;
                }

                if (!userScores.ContainsKey(result.PartnerUserId))
                {
                    userScores[result.PartnerUserId] = 0;
                }

                if (result.IsAnswerCorrectAboutPartner)
                {
                    userScores[result.UserId] += 10; 
                }

                if (result.IsBothMatch)
                {
                    userScores[result.UserId] += 10;
                    userScores[result.PartnerUserId] += 10;
                }
                else
                {
                    if (result.IsAnswerCorrectAboutPartner)
                    {
                        userScores[result.PartnerUserId] += 10;
                    }
                }
            }

            var userIds = userScores.Keys.ToList();
            var userDetails = await authRepository.GetUsersByIds(userIds); 
            var response = userScores.Select(score => {
                var user = userDetails.FirstOrDefault(u => u.GlobalId == score.Key);
                return new UserScoreResponseDTO
                {
                    UserId = score.Key,
                    Score = score.Value,
                    Name = user.Name,
                    ProfileImageUrl = user.ProfileImage 
                };
            }).ToList();

            return response;
        }

    }

}

