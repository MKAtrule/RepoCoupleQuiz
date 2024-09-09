using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Services
{
    public class PartnerInvitationService
    {
        private readonly IPartnerInvitationRepository partnerInvitationRepository;
        private readonly IAuthRepository authRepository;
        public PartnerInvitationService(IPartnerInvitationRepository partnerInvitationRepository, IAuthRepository authRepository)
        {
            this.partnerInvitationRepository = partnerInvitationRepository;
            this.authRepository = authRepository;
        }
        public async Task<GenerateCodeResponseDTO> GenerateCode(GenerateCodeRequestDTO request)
        {
            var userExist= await authRepository.GetByIdAsync(request.UserId);
            if (userExist != null)
            {
                var partnerInvitation = new PartnerInvitation()
                {
                    SenderUserId = request.UserId,
                    InvitationCode = Guid.NewGuid(),
                    IsCodeUsed = false,
                    IsAccepted = false,
                    Active = true,
                };
                var newPartnerInvitation = await partnerInvitationRepository.Create(partnerInvitation);
                return new GenerateCodeResponseDTO
                {
                    UserId = newPartnerInvitation.SenderUserId,
                    Code = newPartnerInvitation.InvitationCode,
                };
            }
            else
            {
                throw new Exception("User with this Id not Found");
            }
        }
        public async Task<PartnerInvitationResponseDTO> PartnerJoining(PartnerInvitationJoiningRequestDTO request)
        {
            var userExist = await authRepository.GetByIdAsync(request.UserId);
            if (userExist != null)
            {
                var isValid = await partnerInvitationRepository.CodeValidation(request.Code);
                if (isValid)
                {
                    var sessionDetails = await partnerInvitationRepository.GetInvitationDetails(request.Code);
                    if(sessionDetails.SenderUserId== request.UserId)
                    {
                        throw new Exception("you can not pair yourself");
                    }
                    sessionDetails.RecieverUserId = request.UserId;
                    sessionDetails.IsCodeUsed = true;
                    sessionDetails.CodeExpires = DateTime.UtcNow;
                    var updatePartnerInvitation = await partnerInvitationRepository.Update(sessionDetails);
                    return new PartnerInvitationResponseDTO
                    {
                        PartnerInvitationId=updatePartnerInvitation.GlobalId,
                        SenderId = updatePartnerInvitation.SenderUserId,
                        SenderName = updatePartnerInvitation.SenderUser.Name,
                        SenderImage = updatePartnerInvitation.SenderUser.ProfileImage,
                        RecieverId = updatePartnerInvitation.RecieverUserId,
                        RecieverImage = updatePartnerInvitation.RecieverUser.ProfileImage,
                        RecieverName = updatePartnerInvitation.RecieverUser.Name,
                    };
                }
                else
                {
                    throw new Exception("Code is not Valid");
                }
            } 
            else
            {
                throw new Exception("User with this Id not Found");
            }

        }
        public async Task<List<PartnerInvitationResponseDTO>> GetAllSessionsAsync()
        {
            var allUsersSession = await partnerInvitationRepository.GetAll();
            if (allUsersSession.Count==0)
            {
                throw new Exception("No any User Session ");
            }
            var response= new List<PartnerInvitationResponseDTO>();
            foreach (var user in allUsersSession)
            {
                if(user.RecieverUser == null)
                {
                    continue;
                }
                response.Add(new PartnerInvitationResponseDTO
                {
                    PartnerInvitationId = user.GlobalId,
                    SenderId=user.SenderUserId,
                    SenderName = user.SenderUser.Name,
                    SenderImage=user.SenderUser.ProfileImage,
                    RecieverId=user.RecieverUserId,
                    RecieverName=user.RecieverUser.Name,
                    RecieverImage=user.RecieverUser.ProfileImage,
                });

            }
            return response;
        }
       

    }
}
