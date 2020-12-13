using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.DataAccess.Migrations
{
    public partial class LikeCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                schema: "Fakebook",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_UserId",
                schema: "Fakebook",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Fakebook",
                table: "Like");

            migrationBuilder.AddPrimaryKey(
                name: "Pk_LikeEntity",
                schema: "Fakebook",
                table: "Like",
                columns: new[] { "UserId", "PostId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Pk_LikeEntity",
                schema: "Fakebook",
                table: "Like");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "Fakebook",
                table: "Like",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                schema: "Fakebook",
                table: "Like",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                schema: "Fakebook",
                table: "Like",
                column: "UserId");
        }
    }
}
