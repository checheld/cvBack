using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class userPhotoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoParamsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhotoParamsPositionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoParamsPositionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoParamsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scale = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoParamsEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoParamsEntity_PhotoParamsPositionEntity_PositionId",
                        column: x => x.PositionId,
                        principalTable: "PhotoParamsPositionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhotoParamsId",
                table: "Users",
                column: "PhotoParamsId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoParamsEntity_PositionId",
                table: "PhotoParamsEntity",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PhotoParamsEntity_PhotoParamsId",
                table: "Users",
                column: "PhotoParamsId",
                principalTable: "PhotoParamsEntity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PhotoParamsEntity_PhotoParamsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PhotoParamsEntity");

            migrationBuilder.DropTable(
                name: "PhotoParamsPositionEntity");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhotoParamsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoParamsId",
                table: "Users");
        }
    }
}
