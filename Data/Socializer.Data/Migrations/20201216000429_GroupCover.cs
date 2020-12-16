namespace Socializer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class GroupCover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CoverImageId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CoverImageId",
                table: "Groups",
                column: "CoverImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Images_CoverImageId",
                table: "Groups",
                column: "CoverImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Images_CoverImageId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CoverImageId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Groups");
        }
    }
}
