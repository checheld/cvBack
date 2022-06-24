using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class oneForManyProjectPhotoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPhotoEntity_Projects_ProjectEntityId",
                table: "ProjectPhotoEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProjectPhotoEntity_ProjectEntityId",
                table: "ProjectPhotoEntity");

            migrationBuilder.DropColumn(
                name: "ProjectEntityId",
                table: "ProjectPhotoEntity");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectPhotoEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhotoEntity_ProjectId",
                table: "ProjectPhotoEntity",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhotoEntity_Projects_ProjectId",
                table: "ProjectPhotoEntity",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectPhotoEntity_Projects_ProjectId",
                table: "ProjectPhotoEntity");

            migrationBuilder.DropIndex(
                name: "IX_ProjectPhotoEntity_ProjectId",
                table: "ProjectPhotoEntity");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectPhotoEntity");

            migrationBuilder.AddColumn<int>(
                name: "ProjectEntityId",
                table: "ProjectPhotoEntity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhotoEntity_ProjectEntityId",
                table: "ProjectPhotoEntity",
                column: "ProjectEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectPhotoEntity_Projects_ProjectEntityId",
                table: "ProjectPhotoEntity",
                column: "ProjectEntityId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
