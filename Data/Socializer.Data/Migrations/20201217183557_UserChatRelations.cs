using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Socializer.Data.Migrations
{
    public partial class UserChatRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChatGroups");

            migrationBuilder.CreateTable(
                name: "ApplicationUserChatGroup",
                columns: table => new
                {
                    ChatGroupsId = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserChatGroup", x => new { x.ChatGroupsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserChatGroup_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserChatGroup_ChatGroups_ChatGroupsId",
                        column: x => x.ChatGroupsId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserChatGroup_MembersId",
                table: "ApplicationUserChatGroup",
                column: "MembersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserChatGroup");

            migrationBuilder.CreateTable(
                name: "UserChatGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatGroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChatGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserChatGroups_ChatGroups_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChatGroups_ChatGroupId",
                table: "UserChatGroups",
                column: "ChatGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatGroups_UserId",
                table: "UserChatGroups",
                column: "UserId");
        }
    }
}
