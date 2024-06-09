using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularUserJob_Jobs_JobId",
                table: "RegularUserJob");

            migrationBuilder.DropForeignKey(
                name: "FK_RegularUserJob_RegularUsers_UserId",
                table: "RegularUserJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegularUserJob",
                table: "RegularUserJob");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49e70972-22c8-441d-8499-068bc5162612");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f760254a-c2f6-4b8b-8240-7e76c5a92b06");

            migrationBuilder.RenameTable(
                name: "RegularUserJob",
                newName: "RegularUserJobs");

            migrationBuilder.RenameIndex(
                name: "IX_RegularUserJob_JobId",
                table: "RegularUserJobs",
                newName: "IX_RegularUserJobs_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegularUserJobs",
                table: "RegularUserJobs",
                columns: new[] { "UserId", "JobId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5fd0f85f-ccc6-4667-bb42-ca0f6a2387b8", null, "Regular", "REGULAR" },
                    { "6902f993-647b-44c7-9593-8c2026a26f6e", null, "Employer", "EMPLOYER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUserJobs_Jobs_JobId",
                table: "RegularUserJobs",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUserJobs_RegularUsers_UserId",
                table: "RegularUserJobs",
                column: "UserId",
                principalTable: "RegularUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularUserJobs_Jobs_JobId",
                table: "RegularUserJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_RegularUserJobs_RegularUsers_UserId",
                table: "RegularUserJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegularUserJobs",
                table: "RegularUserJobs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fd0f85f-ccc6-4667-bb42-ca0f6a2387b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6902f993-647b-44c7-9593-8c2026a26f6e");

            migrationBuilder.RenameTable(
                name: "RegularUserJobs",
                newName: "RegularUserJob");

            migrationBuilder.RenameIndex(
                name: "IX_RegularUserJobs_JobId",
                table: "RegularUserJob",
                newName: "IX_RegularUserJob_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegularUserJob",
                table: "RegularUserJob",
                columns: new[] { "UserId", "JobId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49e70972-22c8-441d-8499-068bc5162612", null, "Regular", "REGULAR" },
                    { "f760254a-c2f6-4b8b-8240-7e76c5a92b06", null, "Employer", "EMPLOYER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUserJob_Jobs_JobId",
                table: "RegularUserJob",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularUserJob_RegularUsers_UserId",
                table: "RegularUserJob",
                column: "UserId",
                principalTable: "RegularUsers",
                principalColumn: "Id");
        }
    }
}
