using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepoCoupleQuiz.Migrations
{
    /// <inheritdoc />
    public partial class initlizedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.GlobalId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.GlobalId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResetPasswordOtp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordOtpExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.GlobalId);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOption",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionOrder = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOption", x => x.GlobalId);
                    table.ForeignKey(
                        name: "FK_QuestionOption_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerInvitation",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecieverUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitationCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsCodeUsed = table.Column<bool>(type: "bit", nullable: false),
                    CodeExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerInvitation", x => x.GlobalId);
                    table.ForeignKey(
                        name: "FK_PartnerInvitation_User_RecieverUserId",
                        column: x => x.RecieverUserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerInvitation_User_SenderUserId",
                        column: x => x.SenderUserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMatch = table.Column<bool>(type: "bit", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.GlobalId);
                    table.ForeignKey(
                        name: "FK_Result_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Result_User_PartnerUserId",
                        column: x => x.PartnerUserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionHistory",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAttempted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionHistory", x => x.GlobalId);
                    table.ForeignKey(
                        name: "FK_SessionHistory_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionHistory_User_PartnerUserId",
                        column: x => x.PartnerUserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.GlobalId);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerForself = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerForPartner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.GlobalId);
                    table.ForeignKey(
                        name: "FK_UserAnswer_QuestionOption_AnswerForPartner",
                        column: x => x.AnswerForPartner,
                        principalTable: "QuestionOption",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAnswer_QuestionOption_AnswerForself",
                        column: x => x.AnswerForself,
                        principalTable: "QuestionOption",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAnswer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswer_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("06766755-71b3-46d4-b87e-f29baf484c15"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a1303db7-ce36-4cbc-91dd-0da197b7b69f"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "GlobalId", "Active", "Age", "CreatedAt", "DeletedAt", "Email", "Gender", "Name", "Password", "ProfileImage", "RefreshToken", "RefreshTokenExpiryTime", "ResetPasswordOtp", "ResetPasswordOtpExpiryTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("108f17ea-6f1b-4223-b20b-3b5186b422a3"), true, 23, new DateTime(2024, 8, 27, 9, 26, 38, 262, DateTimeKind.Utc).AddTicks(3245), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@example.com", "Male", "user", "user123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5057fedd-4ce8-4da5-94ee-709c64bf03a6"), true, 23, new DateTime(2024, 8, 27, 9, 26, 38, 262, DateTimeKind.Utc).AddTicks(3240), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Male", "admin", "admin123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("012a2fe8-daec-487f-859e-eedbd05aefef"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("06766755-71b3-46d4-b87e-f29baf484c15"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5057fedd-4ce8-4da5-94ee-709c64bf03a6") },
                    { new Guid("346b3f6d-aa31-4ce5-8a8d-8b794cef5375"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a1303db7-ce36-4cbc-91dd-0da197b7b69f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("108f17ea-6f1b-4223-b20b-3b5186b422a3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerInvitation_RecieverUserId",
                table: "PartnerInvitation",
                column: "RecieverUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerInvitation_SenderUserId",
                table: "PartnerInvitation",
                column: "SenderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_QuestionId",
                table: "QuestionOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_PartnerUserId",
                table: "Result",
                column: "PartnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_QuestionId",
                table: "Result",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_UserId",
                table: "Result",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionHistory_PartnerUserId",
                table: "SessionHistory",
                column: "PartnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionHistory_QuestionId",
                table: "SessionHistory",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionHistory_UserId",
                table: "SessionHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_AnswerForPartner",
                table: "UserAnswer",
                column: "AnswerForPartner");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_AnswerForself",
                table: "UserAnswer",
                column: "AnswerForself");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_QuestionId",
                table: "UserAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_UserId",
                table: "UserAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerInvitation");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "SessionHistory");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "QuestionOption");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
