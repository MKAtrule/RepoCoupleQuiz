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
            var optionResponselist = new List<OptionRequestDTO>();
            foreach(var option in options)
            {
             var newOption=  await questionOptionRepository.Create(option);
                var OptionResponse = new OptionRequestDTO()
                {
                    OptionId = newOption.GlobalId,
                    Text = newOption.OptionText,
                };
                optionResponselist.Add(OptionResponse);
            }
            return new QuestionResponseDTO
            {
                QuestionId=newQuestion.GlobalId,
                Text=newQuestion.QuestionText,
                Options=optionResponselist.Select(op=>new OptionRequestDTO 
                {
                OptionId=op.OptionId,
                Text=op.Text,
                }).ToList()

            };

        }
        public async Task<QuestionResponseDTO> GetQuestionById(Guid id)
        {
            var question = await questionRepository.GetById(id);
            if(question is null)
            {
                throw new Exception($"Question with this {id} not exist");
            }
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
            var updatedQuestion = await questionRepository.Update(question);    

            var existingOptions = await questionOptionRepository.GetOptionsByQuestionId(updatedQuestion.GlobalId);

            var updatedOptions = new List<OptionRequestDTO>();
            foreach (var option in existingOptions)
            {
                var Option = await questionOptionRepository.GetOptionById(option.GlobalId);
                var requestUpdatingOption= request.Options.Where(op=>op.OptionId==Option.GlobalId).FirstOrDefault();
                if (requestUpdatingOption == null){
                    throw new Exception($" Option ID  not Found");
                }
                Option.OptionText = requestUpdatingOption.Text;
                Option.UpdatedAt= System.DateTime.Now;
                var updatedOption= await questionOptionRepository.Update(Option);
                var optionResponse = new OptionRequestDTO
                {
                    OptionId = updatedOption.GlobalId,
                    Text=updatedOption.OptionText,
                };
                updatedOptions.Add(optionResponse);
            }
            return new QuestionResponseDTO
            {
                 QuestionId=updatedQuestion.GlobalId,
                 Text=updatedQuestion.QuestionText,
                 Options=updatedOptions.Select(op=>new OptionRequestDTO
                 { 
                 OptionId=op.OptionId,
                 Text=op.Text,
                 }
                 ).ToList()
            };

        }

        public async Task<QuestionResponseDTO> SendDailyQuestionAsync()
        {
            var today = DateTime.UtcNow.Date;
            var sentToday = await sentQuestionRepository.GetSentQuestionsByDateAsync(today);

            if (sentToday.Any())
            {
                throw new Exception("The question of the day has already been sent.");
            }

            var sentQuestionIds = await sentQuestionRepository.GetSentQuestionIdsAsync();
            var unsentQuestions = await questionRepository.GetAll();

            var availableQuestions = unsentQuestions
                .Where(q => !sentQuestionIds.Contains(q.GlobalId))
                .ToList();

            if (!availableQuestions.Any())
            {
                throw new Exception("All questions have been sent. No more questions available.");
            }

            var random = new Random();
            var randomIndex = random.Next(availableQuestions.Count);
            var selectedQuestion = availableQuestions[randomIndex];

            await sentQuestionRepository.MarkAsSentAsync(selectedQuestion.GlobalId, today); 

             var response = new QuestionResponseDTO
            {
                QuestionId = selectedQuestion.GlobalId,
                Text = selectedQuestion.QuestionText,
                Options = selectedQuestion.QuestionOption.Select(
                    qo => new OptionRequestDTO
                    {
                        OptionId = qo.GlobalId,
                        Text = qo.OptionText
                    }).ToList(),
            };

            return response;
        }


    }
}
