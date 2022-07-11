using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class photoParamsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoParamsEntity_PhotoParamsPositionEntity_PositionId",
                table: "PhotoParamsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PhotoParamsEntity_PhotoParamsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PhotoParamsPositionEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoParamsEntity",
                table: "PhotoParamsEntity");

            migrationBuilder.DropIndex(
                name: "IX_PhotoParamsEntity_PositionId",
                table: "PhotoParamsEntity");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "PhotoParamsEntity");

            migrationBuilder.RenameTable(
                name: "PhotoParamsEntity",
                newName: "PhotoParams");

            migrationBuilder.AlterColumn<double>(
                name: "Scale",
                table: "PhotoParams",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "PositionX",
                table: "PhotoParams",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PositionY",
                table: "PhotoParams",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoParams",
                table: "PhotoParams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PhotoParams_PhotoParamsId",
                table: "Users",
                column: "PhotoParamsId",
                principalTable: "PhotoParams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PhotoParams_PhotoParamsId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoParams",
                table: "PhotoParams");

            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "PhotoParams");

            migrationBuilder.DropColumn(
                name: "PositionY",
                table: "PhotoParams");

            migrationBuilder.RenameTable(
                name: "PhotoParams",
                newName: "PhotoParamsEntity");

            migrationBuilder.AlterColumn<int>(
                name: "Scale",
                table: "PhotoParamsEntity",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "PhotoParamsEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoParamsEntity",
                table: "PhotoParamsEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PhotoParamsPositionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoParamsPositionEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoParamsEntity_PositionId",
                table: "PhotoParamsEntity",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoParamsEntity_PhotoParamsPositionEntity_PositionId",
                table: "PhotoParamsEntity",
                column: "PositionId",
                principalTable: "PhotoParamsPositionEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PhotoParamsEntity_PhotoParamsId",
                table: "Users",
                column: "PhotoParamsId",
                principalTable: "PhotoParamsEntity",
                principalColumn: "Id");
        }
    }
}
