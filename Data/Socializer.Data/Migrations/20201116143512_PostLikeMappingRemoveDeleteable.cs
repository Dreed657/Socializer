namespace Socializer.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PostLikeMappingRemoveDeleteable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostsLikes_IsDeleted",
                table: "PostsLikes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PostsLikes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PostsLikes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PostsLikes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PostsLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PostsLikes_IsDeleted",
                table: "PostsLikes",
                column: "IsDeleted");
        }
    }
}
