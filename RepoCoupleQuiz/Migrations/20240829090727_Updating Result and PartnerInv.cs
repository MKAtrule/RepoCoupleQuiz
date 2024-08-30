using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepoCoupleQuiz.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingResultandPartnerInv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("e574ca83-f58f-4a31-9abb-dac1c550097c"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("ea9b40e4-e1ff-4789-bb56-e8193a87da89"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("07474320-84d3-4a11-93c6-451d9eda4917"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("27de20da-4899-4aa4-90ae-9eaee65f798c"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("3b7629df-2b28-4177-b7b7-4d47eab1d985"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("d33fdcd7-dc46-494e-9cb2-18e9691a7761"));

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "QuestionOption");

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerInvitationId",
                table: "UserAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerInvitationId",
                table: "SessionHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerInvitationId",
                table: "Result",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("571717da-d644-4430-b7f1-abd933d4f4f6"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("ea19b570-5be3-4b5f-8dac-56fc665dff83"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "GlobalId", "Active", "Age", "CreatedAt", "DeletedAt", "Email", "Gender", "Name", "Password", "ProfileImage", "RefreshToken", "RefreshTokenExpiryTime", "ResetPasswordOtp", "ResetPasswordOtpExpiryTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22b900ac-4b4d-459c-83df-695e9d9e7533"), true, 23, new DateTime(2024, 8, 29, 9, 7, 27, 198, DateTimeKind.Utc).AddTicks(3080), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Male", "admin", "admin123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fa700bd3-52c6-4ef2-83a9-98ab822c95aa"), true, 23, new DateTime(2024, 8, 29, 9, 7, 27, 198, DateTimeKind.Utc).AddTicks(3084), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@example.com", "Male", "user", "user123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("aa081cd6-4e60-4cd2-a0d0-d852472af45a"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ea19b570-5be3-4b5f-8dac-56fc665dff83"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22b900ac-4b4d-459c-83df-695e9d9e7533") },
                    { new Guid("cb485d5e-24f3-4aea-99c4-964351327040"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("571717da-d644-4430-b7f1-abd933d4f4f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fa700bd3-52c6-4ef2-83a9-98ab822c95aa") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_PartnerInvitationId",
                table: "UserAnswer",
                column: "PartnerInvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionHistory_PartnerInvitationId",
                table: "SessionHistory",
                column: "PartnerInvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_PartnerInvitationId",
                table: "Result",
                column: "PartnerInvitationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_PartnerInvitation_PartnerInvitationId",
                table: "Result",
                column: "PartnerInvitationId",
                principalTable: "PartnerInvitation",
                principalColumn: "GlobalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionHistory_PartnerInvitation_PartnerInvitationId",
                table: "SessionHistory",
                column: "PartnerInvitationId",
                principalTable: "PartnerInvitation",
                principalColumn: "GlobalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_PartnerInvitation_PartnerInvitationId",
                table: "UserAnswer",
                column: "PartnerInvitationId",
                principalTable: "PartnerInvitation",
                principalColumn: "GlobalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_PartnerInvitation_PartnerInvitationId",
                table: "Result");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionHistory_PartnerInvitation_PartnerInvitationId",
                table: "SessionHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_PartnerInvitation_PartnerInvitationId",
                table: "UserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_PartnerInvitationId",
                table: "UserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_SessionHistory_PartnerInvitationId",
                table: "SessionHistory");

            migrationBuilder.DropIndex(
                name: "IX_Result_PartnerInvitationId",
                table: "Result");

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("aa081cd6-4e60-4cd2-a0d0-d852472af45a"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("cb485d5e-24f3-4aea-99c4-964351327040"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("571717da-d644-4430-b7f1-abd933d4f4f6"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("ea19b570-5be3-4b5f-8dac-56fc665dff83"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("22b900ac-4b4d-459c-83df-695e9d9e7533"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("fa700bd3-52c6-4ef2-83a9-98ab822c95aa"));

            migrationBuilder.DropColumn(
                name: "PartnerInvitationId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "PartnerInvitationId",
                table: "SessionHistory");

            migrationBuilder.DropColumn(
                name: "PartnerInvitationId",
                table: "Result");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "QuestionOption",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("07474320-84d3-4a11-93c6-451d9eda4917"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("27de20da-4899-4aa4-90ae-9eaee65f798c"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "GlobalId", "Active", "Age", "CreatedAt", "DeletedAt", "Email", "Gender", "Name", "Password", "ProfileImage", "RefreshToken", "RefreshTokenExpiryTime", "ResetPasswordOtp", "ResetPasswordOtpExpiryTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("3b7629df-2b28-4177-b7b7-4d47eab1d985"), true, 23, new DateTime(2024, 8, 28, 12, 0, 45, 598, DateTimeKind.Utc).AddTicks(1046), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@example.com", "Male", "user", "user123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d33fdcd7-dc46-494e-9cb2-18e9691a7761"), true, 23, new DateTime(2024, 8, 28, 12, 0, 45, 598, DateTimeKind.Utc).AddTicks(1026), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Male", "admin", "admin123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("e574ca83-f58f-4a31-9abb-dac1c550097c"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("07474320-84d3-4a11-93c6-451d9eda4917"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d33fdcd7-dc46-494e-9cb2-18e9691a7761") },
                    { new Guid("ea9b40e4-e1ff-4789-bb56-e8193a87da89"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("27de20da-4899-4aa4-90ae-9eaee65f798c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("3b7629df-2b28-4177-b7b7-4d47eab1d985") }
                });
        }
    }
}
