using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class appuseremployer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af8f42a7-e7b9-4cee-9205-fffaba0d5c5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8226a20-b78c-4887-8e50-451021aa4d0b");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Employers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "533be840-fbbb-4d85-8406-7d29c54ab7e9", null, "Regular", "REGULAR" },
                    { "b60621aa-bbf5-4046-96a0-0db9b73bce80", null, "Employer", "EMPLOYER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employers_AppUserId",
                table: "Employers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employers_AspNetUsers_AppUserId",
                table: "Employers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employers_AspNetUsers_AppUserId",
                table: "Employers");

            migrationBuilder.DropIndex(
                name: "IX_Employers_AppUserId",
                table: "Employers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "533be840-fbbb-4d85-8406-7d29c54ab7e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b60621aa-bbf5-4046-96a0-0db9b73bce80");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Employers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "af8f42a7-e7b9-4cee-9205-fffaba0d5c5d", null, "Regular", "REGULAR" },
                    { "e8226a20-b78c-4887-8e50-451021aa4d0b", null, "Employer", "EMPLOYER" }
                });
        }
    }
}
