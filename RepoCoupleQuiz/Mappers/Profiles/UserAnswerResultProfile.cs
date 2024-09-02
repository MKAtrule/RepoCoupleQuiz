using AutoMapper;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Mappers.Profiles
{
    public class UserAnswerResultProfile:Profile
    {
        public UserAnswerResultProfile()
        {
            CreateMap<Result, UserAnswerResultResponseDTO>()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
           
              .ForMember(dest => dest.PartnerUserName, opt => opt.MapFrom(src => src.PartnerUser.Name))
              //      .ForMember(dest => dest.IsAnswerCorrectAboutPartner, opt => opt.MapFrom(src => src.IsMatch))
              .ForMember(dest => dest.AreBothAnswersMatching, opt => opt.MapFrom(src => src.IsBothMatch))
              .ReverseMap();
             // .ForMember(dest => dest.UserScore, opt => opt.MapFrom(src => src.UserScore))
            //  .ForMember(dest => dest.PartnerScore, opt => opt.MapFrom(src => src.PartnerScore));
        }
    }
}
