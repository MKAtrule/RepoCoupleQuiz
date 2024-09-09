using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepoCoupleQuiz.Config;
using RepoCoupleQuiz.Controllers;
using RepoCoupleQuiz.Data;
using RepoCoupleQuiz.Services;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ServiceConfiguration.RegisterServices(builder.Services);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfireServer();  
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        RoleClaimType = ClaimTypes.Role,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
    x.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseHangfireDashboard();  
app.UseHangfireServer();    
var recurringJobs = app.Services.GetRequiredService<IRecurringJobManager>();
recurringJobs.AddOrUpdate<QuestionController>(
    "Send Daily Question",
    service => service.SendDailyQuestion(),
    Cron.DayInterval(1));
recurringJobs.AddOrUpdate<SessionHistoryService>(
    "Add SessionHistory into User",
    service=>service.AddQuestionToSessionHistory(),
   Cron.DayInterval(1) );
app.MapControllers();

app.Run();
