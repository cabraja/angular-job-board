using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addregularuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20b3d1eb-f251-4f7f-8d92-b8406b912622");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0e013d2-5ad2-47ce-bf8a-06ce57050268");

            migrationBuilder.AddColumn<int>(
                name: "RegularUserId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegularUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularUsers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c1f27401-9eaa-4a37-a021-e51f629ac24c", null, "Employer", "EMPLOYER" },
                    { "c2c3842f-86d4-452f-8cbc-598458cfff24", null, "Regular", "REGULAR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RegularUserId",
                table: "Jobs",
                column: "RegularUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegularUsers_AppUserId",
                table: "RegularUsers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_RegularUsers_RegularUserId",
                table: "Jobs",
                column: "RegularUserId",
                principalTable: "RegularUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_RegularUsers_RegularUserId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "RegularUsers");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_RegularUserId",
                table: "Jobs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1f27401-9eaa-4a37-a021-e51f629ac24c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c2c3842f-86d4-452f-8cbc-598458cfff24");

            migrationBuilder.DropColumn(
                name: "RegularUserId",
                table: "Jobs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20b3d1eb-f251-4f7f-8d92-b8406b912622", null, "Employer", "EMPLOYER" },
                    { "d0e013d2-5ad2-47ce-bf8a-06ce57050268", null, "Regular", "REGULAR" }
                });
        }
    }
}
