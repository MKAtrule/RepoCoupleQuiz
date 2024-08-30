﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepoCoupleQuiz.Data;

#nullable disable

namespace RepoCoupleQuiz.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240829090727_Updating Result and PartnerInv")]
    partial class UpdatingResultandPartnerInv
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RepoCoupleQuiz.Models.PartnerInvitation", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CodeExpires")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("InvitationCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCodeUsed")
                        .HasColumnType("bit");

                    b.Property<Guid?>("RecieverUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("GlobalId");

                    b.HasIndex("RecieverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("PartnerInvitation");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.Question", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("GlobalId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.QuestionOption", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("OptionOrder")
                        .HasColumnType("int");

                    b.Property<string>("OptionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("GlobalId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionOption");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.Result", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMatch")
                        .HasColumnType("bit");

                    b.Property<Guid>("PartnerInvitationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PartnerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GlobalId");

                    b.HasIndex("PartnerInvitationId");

                    b.HasIndex("PartnerUserId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Result");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.Role", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("GlobalId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            GlobalId = new Guid("ea19b570-5be3-4b5f-8dac-56fc665dff83"),
                            Active = false,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "Admin",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            GlobalId = new Guid("571717da-d644-4430-b7f1-abd933d4f4f6"),
                            Active = false,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleName = "User",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.SentQuestion", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("GlobalId");

                    b.HasIndex("QuestionId");

                    b.ToTable("SentQuestion");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.SessionHistory", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAttempted")
                        .HasColumnType("bit");

                    b.Property<Guid>("PartnerInvitationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PartnerUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GlobalId");

                    b.HasIndex("PartnerInvitationId");

                    b.HasIndex("PartnerUserId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("SessionHistory");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.User", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResetPasswordOtp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetPasswordOtpExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("GlobalId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            GlobalId = new Guid("22b900ac-4b4d-459c-83df-695e9d9e7533"),
                            Active = true,
                            Age = 23,
                            CreatedAt = new DateTime(2024, 8, 29, 9, 7, 27, 198, DateTimeKind.Utc).AddTicks(3080),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@example.com",
                            Gender = "Male",
                            Name = "admin",
                            Password = "admin123",
                            ProfileImage = "...",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            GlobalId = new Guid("fa700bd3-52c6-4ef2-83a9-98ab822c95aa"),
                            Active = true,
                            Age = 23,
                            CreatedAt = new DateTime(2024, 8, 29, 9, 7, 27, 198, DateTimeKind.Utc).AddTicks(3084),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "user@example.com",
                            Gender = "Male",
                            Name = "user",
                            Password = "user123",
                            ProfileImage = "...",
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.UserAnswers", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("AnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("AnswerForPartner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnswerForself")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PartnerInvitationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GlobalId");

                    b.HasIndex("AnswerForPartner");

                    b.HasIndex("AnswerForself");

                    b.HasIndex("PartnerInvitationId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswer");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.UserRole", b =>
                {
                    b.Property<Guid>("GlobalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GlobalId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            GlobalId = new Guid("aa081cd6-4e60-4cd2-a0d0-d852472af45a"),
                            Active = false,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleId = new Guid("ea19b570-5be3-4b5f-8dac-56fc665dff83"),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("22b900ac-4b4d-459c-83df-695e9d9e7533")
                        },
                        new
                        {
                            GlobalId = new Guid("cb485d5e-24f3-4aea-99c4-964351327040"),
                            Active = false,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RoleId = new Guid("571717da-d644-4430-b7f1-abd933d4f4f6"),
                            UpdatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("fa700bd3-52c6-4ef2-83a9-98ab822c95aa")
                        });
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.PartnerInvitation", b =>
                {
                    b.HasOne("RepoCoupleQuiz.Models.User", "RecieverUser")
                        .WithMany("ReceivedInvitation")
                        .HasForeignKey("RecieverUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RepoCoupleQuiz.Models.User", "SenderUser")
                        .WithMany("SentInvitation")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RecieverUser");

                    b.Navigation("SenderUser");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.QuestionOption", b =>
                {
                    b.HasOne("RepoCoupleQuiz.Models.Question", "Question")
                        .WithMany("QuestionOption")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.Result", b =>
                {
                    b.HasOne("RepoCoupleQuiz.Models.PartnerInvitation", "PartnerInvitation")
                        .WithMany()
                        .HasForeignKey("PartnerInvitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.User", "PartnerUser")
                        .WithMany()
                        .HasForeignKey("PartnerUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.Question", "Question")
                        .WithMany("Result")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.User", "User")
                        .WithMany("Result")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PartnerInvitation");

                    b.Navigation("PartnerUser");

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.SentQuestion", b =>
                {
                    b.HasOne("RepoCoupleQuiz.Models.Question", "Question")
                        .WithMany("SentQuestion")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.SessionHistory", b =>
                {
                    b.HasOne("RepoCoupleQuiz.Models.PartnerInvitation", "PartnerInvitation")
                        .WithMany()
                        .HasForeignKey("PartnerInvitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.User", "PartnerUser")
                        .WithMany()
                        .HasForeignKey("PartnerUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.Question", "Question")
                        .WithMany("SessionHistory")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.User", "User")
                        .WithMany("SessionHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PartnerInvitation");

                    b.Navigation("PartnerUser");

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.UserAnswers", b =>
                {
                    b.HasOne("RepoCoupleQuiz.Models.QuestionOption", "AnswerPartnerOption")
                        .WithMany("UserAnswersPartner")
                        .HasForeignKey("AnswerForPartner")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.QuestionOption", "AnswerSelfOption")
                        .WithMany("UserAnswersSelf")
                        .HasForeignKey("AnswerForself")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.PartnerInvitation", "PartnerInvitation")
                        .WithMany("UserAnswers")
                        .HasForeignKey("PartnerInvitationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.Question", "Question")
                        .WithMany("UserAnswer")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.User", "User")
                        .WithMany("UserAnswer")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerPartnerOption");

                    b.Navigation("AnswerSelfOption");

                    b.Navigation("PartnerInvitation");

                    b.Navigation("Question");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.UserRole", b =>
                {
                    b.HasOne("RepoCoupleQuiz.Models.Role", "Role")
                        .WithMany("UserRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepoCoupleQuiz.Models.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.PartnerInvitation", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.Question", b =>
                {
                    b.Navigation("QuestionOption");

                    b.Navigation("Result");

                    b.Navigation("SentQuestion");

                    b.Navigation("SessionHistory");

                    b.Navigation("UserAnswer");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.QuestionOption", b =>
                {
                    b.Navigation("UserAnswersPartner");

                    b.Navigation("UserAnswersSelf");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.Role", b =>
                {
                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("RepoCoupleQuiz.Models.User", b =>
                {
                    b.Navigation("ReceivedInvitation");

                    b.Navigation("Result");

                    b.Navigation("SentInvitation");

                    b.Navigation("SessionHistory");

                    b.Navigation("UserAnswer");

                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
