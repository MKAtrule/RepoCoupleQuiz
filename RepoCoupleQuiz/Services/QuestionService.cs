using AutoMapper;
using Microsoft.Extensions.Options;
using RepoCoupleQuiz.DTO.RequestDTO;
using RepoCoupleQuiz.DTO.ResponseDTO;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Services
{
    public class QuestionService
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IQuestionOptionRepository questionOptionRepository;
        private readonly IMapper mapper;
        public QuestionService(IQuestionRepository questionRepository, IQuestionOptionRepository questionOptionRepository, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.questionOptionRepository = questionOptionRepository;
            this.mapper = mapper;
        }
        public async Task<QuestionResponseDTO> CreateAsync(QuestionRequestDTO request)
        {
            var question = mapper.Map<Question>(request);
            question.Active = true; 
            var newQuestion = await questionRepository.Create(question);
             var options= request.Options.Select(op => new
             QuestionOption{
                QuestionId=newQuestion.GlobalId,
                OptionText=op.Text,
                IsCorrect=op.IsCorrect,
                Active=true
              }
             ).ToList();
            await questionOptionRepository.AddOptions(options);
            return new QuestionResponseDTO
            {
                QuestionId=newQuestion.GlobalId,
                Text=newQuestion.QuestionText,
                Options=request.Options,

            };

        }
        public async Task<QuestionResponseDTO> GetQuestionById(Guid id)
        {
            var question = await questionRepository.GetById(id);
            var options= question.QuestionOption.Select(op =>new OptionRequestDTO { Text=op.OptionText,IsCorrect=op.IsCorrect}).ToList();
            List<OptionRequestDTO> OptionsText= mapper.Map<List<OptionRequestDTO>>(options);
            return new QuestionResponseDTO
            {
                QuestionId = question.GlobalId,
                Text=question.QuestionText,
                Options=OptionsText,
            };
        }

        public async Task<QuestionResponseDTO> UpdateAsync(UpdateQuestionRequestDTO request)
        {
            var question = await questionRepository.GetById(request.QuestionId);

            if (question == null)
            {
                throw new Exception("Question not found");
            }

            question.QuestionText = request.Text;

            var existingOptions = await questionOptionRepository.GetOptionsByQuestionId(request.QuestionId);

            var updatedOptions = new List<QuestionOption>();

            foreach (var optionDto in request.Options)
            {
                var existingOption = existingOptions
                    .FirstOrDefault(eo => eo.OptionText == optionDto.Text);

                if (existingOption != null)
                {
                    existingOption.IsCorrect = optionDto.IsCorrect;
                    updatedOptions.Add(existingOption);
                }
                else
                {
                    var newOption = new QuestionOption
                    {
                        QuestionId = question.GlobalId,
                        OptionText = optionDto.Text,
                        IsCorrect = optionDto.IsCorrect,
                        Active = true
                    };
                    updatedOptions.Add(newOption);
                }
            }

            var optionsToRemove = existingOptions
                .Where(eo => !request.Options.Any(o => o.Text == eo.OptionText))
                .ToList();

            await questionRepository.Update(question);

            if (optionsToRemove.Any())
            {
                foreach (var option in optionsToRemove)
                {
                    await questionOptionRepository.Delete(option);
                }
            }

            await questionOptionRepository.UpdateOptions(updatedOptions);

            return new QuestionResponseDTO
            {
                QuestionId = question.GlobalId,
                Text = question.QuestionText,
                Options = request.Options
            };
        }

        //public async Task<QuestionResponseDTO> UpdateQuestionAsync(UpdateQuestionRequestDTO request)
        //{
        //    var question = await questionRepository.GetById(request.QuestionId);
        //    if (question != null)
        //    {
        //        // Map the incoming request DTO to the existing question entity
        //        mapper.Map(request, question);

        //        // Update the question in the repository
        //        var newQuestion = await questionRepository.Update(question);

        //        // Get and update the related options
        //        var options = await questionOptionRepository.GetOptionsByQuestionId(request.QuestionId);
        //        mapper.Map(request.Options, options);
        //        await questionOptionRepository.UpdateOptions(options);

        //        return mapper.Map<QuestionResponseDTO>(newQuestion);
        //    }
        //    else
        //    {
        //        throw new Exception("Question not found");
        //    }
        //}
    }
}
