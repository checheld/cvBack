using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeviossaCV.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 1, new DateTime(2022, 3, 3, 15, 22, 24, 902, DateTimeKind.Local).AddTicks(8487), "qwerty" });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[] { 2, new DateTime(2022, 3, 3, 15, 22, 24, 903, DateTimeKind.Local).AddTicks(4233), "asdfg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
