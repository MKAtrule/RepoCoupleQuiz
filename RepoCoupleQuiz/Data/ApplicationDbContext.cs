using Microsoft.EntityFrameworkCore;
using RepoCoupleQuiz.Models;

namespace RepoCoupleQuiz.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionOption> QuestionOption { get; set; }
        public DbSet<UserAnswers> UserAnswer { get; set; }
        public DbSet<SessionHistory> SessionHistory { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<SentQuestion> SentQuestion { get; set; }
        public DbSet<PartnerInvitation> PartnerInvitation { get; set; }
        //  public DbSet<Progress> Progresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring UserAnswers relationships
            modelBuilder.Entity<UserAnswers>()
                .HasOne(ua => ua.AnswerSelfOption)
                .WithMany(qo => qo.UserAnswersSelf)
                .HasForeignKey(ua => ua.AnswerForself)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAnswers>()
                .HasOne(ua => ua.AnswerPartnerOption)
                .WithMany(qo => qo.UserAnswersPartner)
                .HasForeignKey(ua => ua.AnswerForPartner)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuring relationships between UserAnswers and User
            modelBuilder.Entity<UserAnswers>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAnswer)
                .HasForeignKey(ua => ua.UserId);

            // Configuring relationships between SessionHistory and User
            modelBuilder.Entity<SessionHistory>()
                .HasOne(sh => sh.User)
                .WithMany(u => u.SessionHistory)
                .HasForeignKey(sh => sh.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SessionHistory>()
                .HasOne(sh => sh.PartnerUser)
                .WithMany()
                .HasForeignKey(sh => sh.PartnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuring Result relationships
            modelBuilder.Entity<Result>()
                .HasOne(r => r.User)
                .WithMany(u => u.Result)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.PartnerUser)
                .WithMany()
                .HasForeignKey(r => r.PartnerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuring PartnerInvitation relationships
            modelBuilder.Entity<PartnerInvitation>()
                .HasOne(pi => pi.SenderUser)
                .WithMany(u => u.SentInvitation)
                .HasForeignKey(pi => pi.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PartnerInvitation>()
                .HasOne(pi => pi.RecieverUser)
                .WithMany(u => u.ReceivedInvitation)
                .HasForeignKey(pi => pi.RecieverUserId)
                .OnDelete(DeleteBehavior.Restrict);
            var adminRoleId = Guid.NewGuid();
            var userRoleId = Guid.NewGuid();

            modelBuilder.Entity<Role>().HasData(
                new Role { GlobalId = adminRoleId, RoleName = "Admin" },
                new Role { GlobalId = userRoleId, RoleName = "User" }
            );

            var adminUserId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    GlobalId = adminUserId,
                    Name = "admin",
                    Email = "admin@example.com",
                    Password = "admin123",
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    Age = 23,
                    Gender = "Male",
                    ProfileImage = "..."
                },
                new User
                {
                    GlobalId = userId,
                    Name = "user",
                    Email = "user@example.com",
                    Password = "user123",
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    Age = 23,
                    Gender ="Male",
                    ProfileImage = "..."
                }
            );
            modelBuilder.Entity<UserRole>().HasData(
               new UserRole
               {
                   GlobalId = Guid.NewGuid(),
                   UserId = adminUserId,
                   RoleId = adminRoleId
               },
               new UserRole
               {
                   GlobalId = Guid.NewGuid(),
                   UserId = userId,
                   RoleId = userRoleId
               }
           );
            base.OnModelCreating(modelBuilder);
        }
    }
}
