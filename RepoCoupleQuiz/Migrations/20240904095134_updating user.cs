using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepoCoupleQuiz.Migrations
{
    /// <inheritdoc />
    public partial class updatinguser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("906e8ec9-62fa-4d92-949e-3adca0a2bcfe"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("e49ade79-58b7-41f9-be06-4f00c4630a98"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("2e95c889-8950-4f7f-81ce-74b59b403923"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("cb30ce4c-64a8-4b55-a909-07f734b3e5c3"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("1ae6feb2-91d1-42a8-8bc4-8adff215ae8f"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("a4bff49a-fe52-4d15-9deb-553d27c878bf"));

            migrationBuilder.AddColumn<bool>(
                name: "IsOTpVerified",
                table: "User",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("21d4ab18-1b81-4821-bf71-05e504479694"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bdfdc0f3-6f68-4560-8f56-e9eec2d8e773"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "GlobalId", "Active", "Age", "CreatedAt", "DeletedAt", "Email", "Gender", "IsOTpVerified", "Name", "Password", "ProfileImage", "RefreshToken", "RefreshTokenExpiryTime", "ResetPasswordOtp", "ResetPasswordOtpExpiryTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("31db4341-068d-481d-a7c1-40df5455fde1"), true, 23, new DateTime(2024, 9, 4, 9, 51, 33, 677, DateTimeKind.Utc).AddTicks(372), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Male", null, "admin", "admin123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fefe2187-18f7-4227-b278-2160c47d4388"), true, 23, new DateTime(2024, 9, 4, 9, 51, 33, 677, DateTimeKind.Utc).AddTicks(375), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@example.com", "Male", null, "user", "user123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("a55adaa6-2d20-4a0a-a540-10bf5e22b465"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("21d4ab18-1b81-4821-bf71-05e504479694"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fefe2187-18f7-4227-b278-2160c47d4388") },
                    { new Guid("cbf00248-6385-4174-a7cb-39a6179a1208"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("bdfdc0f3-6f68-4560-8f56-e9eec2d8e773"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("31db4341-068d-481d-a7c1-40df5455fde1") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("a55adaa6-2d20-4a0a-a540-10bf5e22b465"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "GlobalId",
                keyValue: new Guid("cbf00248-6385-4174-a7cb-39a6179a1208"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("21d4ab18-1b81-4821-bf71-05e504479694"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "GlobalId",
                keyValue: new Guid("bdfdc0f3-6f68-4560-8f56-e9eec2d8e773"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("31db4341-068d-481d-a7c1-40df5455fde1"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "GlobalId",
                keyValue: new Guid("fefe2187-18f7-4227-b278-2160c47d4388"));

            migrationBuilder.DropColumn(
                name: "IsOTpVerified",
                table: "User");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2e95c889-8950-4f7f-81ce-74b59b403923"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("cb30ce4c-64a8-4b55-a909-07f734b3e5c3"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "GlobalId", "Active", "Age", "CreatedAt", "DeletedAt", "Email", "Gender", "Name", "Password", "ProfileImage", "RefreshToken", "RefreshTokenExpiryTime", "ResetPasswordOtp", "ResetPasswordOtpExpiryTime", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1ae6feb2-91d1-42a8-8bc4-8adff215ae8f"), true, 23, new DateTime(2024, 8, 30, 10, 36, 7, 389, DateTimeKind.Utc).AddTicks(2185), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@example.com", "Male", "user", "user123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a4bff49a-fe52-4d15-9deb-553d27c878bf"), true, 23, new DateTime(2024, 8, 30, 10, 36, 7, 389, DateTimeKind.Utc).AddTicks(2181), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", "Male", "admin", "admin123", "...", null, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "GlobalId", "Active", "CreatedAt", "DeletedAt", "RoleId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("906e8ec9-62fa-4d92-949e-3adca0a2bcfe"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2e95c889-8950-4f7f-81ce-74b59b403923"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1ae6feb2-91d1-42a8-8bc4-8adff215ae8f") },
                    { new Guid("e49ade79-58b7-41f9-be06-4f00c4630a98"), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cb30ce4c-64a8-4b55-a909-07f734b3e5c3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a4bff49a-fe52-4d15-9deb-553d27c878bf") }
                });
        }
    }
}
