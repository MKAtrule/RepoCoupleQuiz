using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepoCoupleQuiz.Migrations
{
    /// <inheritdoc />
    public partial class addsentquestiontbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("012a2fe8-daec-487f-859e-eedbd05aefef"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("346b3f6d-aa31-4ce5-8a8d-8b794cef5375"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("06766755-71b3-46d4-b87e-f29baf484c15"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("a1303db7-ce36-4cbc-91dd-0da197b7b69f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("108f17ea-6f1b-4223-b20b-3b5186b422a3"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("5057fedd-4ce8-4da5-94ee-709c64bf03a6"));

            migrationBuilder.CreateTable(
                name: "SentQuestion",
                columns: table => new
                {
                    GlobalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentQuestion", x => x.GlobalId);
                    table.ForeignKey(
                        name: "FK_SentQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "GlobalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("9108c8a5-7e49-4edc-bce2-97b5c2d7a0be"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("becc6319-ebe3-483e-a3f0-7d23c1b90655"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "GlobalId", "Active", "Age", "CreatedAt", "DeletedAt", "Email", "Gender", "Name", "Password", "ProfileImage", "RefreshToken", "RefreshTokenExpiryTime", "ResetPasswordOtp", "ResetPasswordOtpExpiryTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("81db4661-3377-47fb-a13a-778ed21ac95d"), true, 23, new DateTime(2024, 8, 28, 9, 8, 59, 470, DateTimeKind.Utc).AddTicks(5487), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@example.com", "Male", "user", "user123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bb17a0e0-e132-463f-b169-7caff02b4f5a"), true, 23, new DateTime(2024, 8, 28, 9, 8, 59, 470, DateTimeKind.Utc).AddTicks(5483), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Male", "admin", "admin123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("a8dad279-bdc7-4696-a265-ce046fbdb86b"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("becc6319-ebe3-483e-a3f0-7d23c1b90655"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bb17a0e0-e132-463f-b169-7caff02b4f5a") },
                    { new Guid("f727797e-0176-4303-9d76-7d9d3acd6b3d"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9108c8a5-7e49-4edc-bce2-97b5c2d7a0be"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("81db4661-3377-47fb-a13a-778ed21ac95d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentQuestion_QuestionId",
                table: "SentQuestion",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SentQuestion");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("a8dad279-bdc7-4696-a265-ce046fbdb86b"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("f727797e-0176-4303-9d76-7d9d3acd6b3d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("9108c8a5-7e49-4edc-bce2-97b5c2d7a0be"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("becc6319-ebe3-483e-a3f0-7d23c1b90655"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("81db4661-3377-47fb-a13a-778ed21ac95d"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("bb17a0e0-e132-463f-b169-7caff02b4f5a"));

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
        }
    }
}
