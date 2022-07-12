using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class connectionStringChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_Companies_CompanyId",
                table: "WorkExperiences");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "WorkExperiences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_Companies_CompanyId",
                table: "WorkExperiences",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_Companies_CompanyId",
                table: "WorkExperiences");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "WorkExperiences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "Educations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_Companies_CompanyId",
                table: "WorkExperiences",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
