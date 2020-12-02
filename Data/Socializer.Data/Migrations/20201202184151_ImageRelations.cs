using Microsoft.EntityFrameworkCore.Migrations;

namespace Socializer.Data.Migrations
{
    public partial class ImageRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoverImageId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CoverImageId1",
                table: "AspNetUsers",
                column: "CoverImageId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId1",
                table: "AspNetUsers",
                column: "ProfileImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_CoverImageId1",
                table: "AspNetUsers",
                column: "CoverImageId1",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ProfileImageId1",
                table: "AspNetUsers",
                column: "ProfileImageId1",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_CoverImageId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ProfileImageId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CoverImageId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoverImageId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageId1",
                table: "AspNetUsers");
        }
    }
}
