using AutoMapper;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Mappers.Profiles
{
    public class QuestionOptionProfile:Profile
    {
        public QuestionOptionProfile()
        {
            CreateMap<QuestionOption,OptionRequestDTO>()
                .ForMember(src=>src.Text,opt=>opt.MapFrom(src=>src.OptionText))
                .ReverseMap();
        }
    }
}
