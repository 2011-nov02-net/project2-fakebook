using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.DataAccess.Migrations
{
    public partial class DateTimeBugFix : Migration
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Fakebook",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdatetime())");

            migrationBuilder.AddForeignKey(
                name: "Fk_Follow_Followee",
                schema: "Fakebook",
                table: "Follow",
                column: "FolloweeId",
                principalSchema: "Fakebook",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "Fk_Follow_Follower",
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
                name: "Fk_Follow_Followee",
                schema: "Fakebook",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "Fk_Follow_Follower",
                schema: "Fakebook",
                table: "Follow");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                schema: "Fakebook",
                table: "Post",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "(getdatetime())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "(getdate())");

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
    }
}
