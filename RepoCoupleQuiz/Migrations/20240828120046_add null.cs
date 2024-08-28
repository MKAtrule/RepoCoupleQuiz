using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepoCoupleQuiz.Migrations
{
    /// <inheritdoc />
    public partial class addnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<Guid>(
                name: "RecieverUserId",
                table: "PartnerInvitation",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "RecieverUserId",
                table: "PartnerInvitation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
        }
    }
}
