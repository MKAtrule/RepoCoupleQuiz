using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RepoCoupleQuiz.Common.File.Interface;
using RepoCoupleQuiz.Common.File.Service.FileService;
using RepoCoupleQuiz.Interface;
using RepoCoupleQuiz.Repository;
using RepoCoupleQuiz.Services;

namespace RepoCoupleQuiz.Config
{
    public class ServiceConfiguration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddSingleton<IFileValidation, FileValidationService>();
            services.AddScoped<IFileUpload, FileService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<ISentQuestionRepository, SentQuestionRepository>();
            services.AddScoped<QuestionService>();  
            services.AddScoped<AuthService>();
            services.AddScoped<IPartnerInvitationRepository, PartnerInvitationRepository>();
            services.AddScoped<PartnerInvitationService>();
            services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
            services.AddAutoMapper(typeof(Program).Assembly);
            services.AddScoped<ISessionHistoryRepository, SessionHistoryRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<UserAnswersService>();
            services.AddScoped<SessionHistoryService>();
            services.AddScoped<ResultService>();
        }
    }
}
