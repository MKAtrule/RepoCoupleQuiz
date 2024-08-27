using AutoMapper;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Mappers.Profiles
{
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionRequestDTO>()
                .ForMember(src=>src.Text,opt=>opt.MapFrom(des=>des.QuestionText))
                .ReverseMap();
            CreateMap<Question, UpdateQuestionRequestDTO>()
               .ForMember(src => src.Text, opt => opt.MapFrom(des => des.QuestionText))
               .ForMember(src => src.QuestionId, opt => opt.MapFrom(des => des.GlobalId))
               .ReverseMap();
            CreateMap<Question,QuestionResponseDTO>()
                .ForMember(src=>src.Text,opt=>opt.MapFrom(des=>des.QuestionText))
                .ForMember(src=>src.QuestionId,opt=>opt.MapFrom(des=>des.GlobalId))
                .ReverseMap();

        }
    }
}
