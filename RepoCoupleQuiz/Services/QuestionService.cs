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
        private readonly ISentQuestionRepository sentQuestionRepository;


        public QuestionService(IQuestionRepository questionRepository, IQuestionOptionRepository questionOptionRepository, IMapper mapper, ISentQuestionRepository sentQuestionRepository)
        {
            this.questionRepository = questionRepository;
            this.questionOptionRepository = questionOptionRepository;
            this.mapper = mapper;
            this.sentQuestionRepository = sentQuestionRepository;
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
            var options= question.QuestionOption.Select(op =>new OptionRequestDTO {OptionId=op.GlobalId ,Text=op.OptionText}).ToList();
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
                    updatedOptions.Add(existingOption);
                }
                else
                {
                    var newOption = new QuestionOption
                    {
                        QuestionId = question.GlobalId,
                        OptionText = optionDto.Text,
           
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

        public async Task<QuestionResponseDTO> SendDailyQuestionAsync()
        {
            var sentQuestionIds = await sentQuestionRepository.GetSentQuestionIdsAsync();
            var unsentQuestions = await questionRepository.GetAll();

            var availableQuestions = unsentQuestions
                .Where(q => !sentQuestionIds.Contains(q.GlobalId))
                .ToList();

            if (!availableQuestions.Any())
            {
                throw new Exception("All questions have been snet No more questions available.");
            }

            var random = new Random();
            var randomIndex = random.Next(availableQuestions.Count);
            var selectedQuestion = availableQuestions[randomIndex];

            await sentQuestionRepository.MarkAsSentAsync(selectedQuestion.GlobalId);
            var response = new QuestionResponseDTO
            { 
            QuestionId=selectedQuestion.GlobalId,
            Text = selectedQuestion.QuestionText,
            Options=selectedQuestion.QuestionOption.Select(
                qo=>new OptionRequestDTO {
                    OptionId=qo.GlobalId,
                    Text=qo.OptionText
                }).ToList(),
            
            };


         //   var response = mapper.Map<QuestionResponseDTO>(selectedQuestion);
            return response;
        }

    }
}
