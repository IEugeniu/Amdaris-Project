using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCoursesWebApi.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "dcd6cff0-9946-4ff8-a4a3-63b4b5ef89ab", "ADMIN" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "bf251b4e-5df7-46c6-a4b4-187db13f7d40", "AUTHOR" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "76a5451d-a511-4ad2-b3d4-de4dc002f5f7", "STUDENT" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "Experience" },
                values: new object[] { "82da3327-8b4a-4c64-af91-c35ad0855e59", "Amdaris" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "Experience" },
                values: new object[] { "3d40b3b8-d87b-421a-b459-e333cedcc058", "Microsoft" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "176ff55c-95c2-404c-8249-c46cfc1cb24b", null });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "814334ee-6464-4ddb-b5d5-2d7b6cb24cac", null });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "8540b33f-2ae4-42b5-a9d2-38aa6e670cff", null });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "Experience" },
                values: new object[] { "4a6933fb-6f38-42f3-8a9f-889a8fdd28c1", "Amdaris" });

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "Experience" },
                values: new object[] { "2e44d795-a4e9-4ce1-aac2-1a66f237d063", "Microsoft" });
        }
    }
}
