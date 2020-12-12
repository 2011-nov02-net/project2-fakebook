using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.DataAccess.Migrations
{
    public partial class AddFollowEntityConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follow_FolloweeId",
                schema: "Fakebook",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_FollowerId",
                schema: "Fakebook",
                table: "Follow");

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_FolloweeId",
                schema: "Fakebook",
                table: "Follow",
                column: "FolloweeId",
                principalSchema: "Fakebook",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_FollowerId",
                schema: "Fakebook",
                table: "Follow",
                column: "FollowerId",
                principalSchema: "Fakebook",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follow_FolloweeId",
                schema: "Fakebook",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_FollowerId",
                schema: "Fakebook",
                table: "Follow");

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_FolloweeId",
                schema: "Fakebook",
                table: "Follow",
                column: "FolloweeId",
                principalSchema: "Fakebook",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_FollowerId",
                schema: "Fakebook",
                table: "Follow",
                column: "FollowerId",
                principalSchema: "Fakebook",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
