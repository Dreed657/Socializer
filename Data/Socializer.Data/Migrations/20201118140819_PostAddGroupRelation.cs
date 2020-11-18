using Microsoft.EntityFrameworkCore.Migrations;

namespace Socializer.Data.Migrations
{
    public partial class PostAddGroupRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InGroup",
                table: "Posts",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InGroup",
                table: "Posts");
        }
    }
}
