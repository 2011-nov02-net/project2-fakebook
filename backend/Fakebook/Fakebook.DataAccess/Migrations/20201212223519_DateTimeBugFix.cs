using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.DataAccess.Migrations
{
    public partial class DateTimeBugFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Fakebook");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Fakebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Follow",
                schema: "Fakebook",
                columns: table => new
                {
                    FollowerId = table.Column<int>(type: "int", nullable: false),
                    FolloweeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_FollowEntity", x => new { x.FollowerId, x.FolloweeId });
                    table.ForeignKey(
                        name: "Fk_Follow_Followee",
                        column: x => x.FolloweeId,
                        principalSchema: "Fakebook",
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Follow_Follower",
                        column: x => x.FollowerId,
                        principalSchema: "Fakebook",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "Fakebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_UserId",
                        column: x => x.UserId,
                        principalSchema: "Fakebook",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "Fakebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "(getdate())"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Comment_Comment",
                        column: x => x.ParentId,
                        principalSchema: "Fakebook",
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_Comment_Post",
                        column: x => x.PostId,
                        principalSchema: "Fakebook",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMMENT_USER",
                        column: x => x.UserId,
                        principalSchema: "Fakebook",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Like",
                schema: "Fakebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Like_Post",
                        column: x => x.PostId,
                        principalSchema: "Fakebook",
                        principalTable: "Post",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Like_User",
                        column: x => x.UserId,
                        principalSchema: "Fakebook",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                schema: "Fakebook",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                schema: "Fakebook",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                schema: "Fakebook",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FolloweeId",
                schema: "Fakebook",
                table: "Follow",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId",
                schema: "Fakebook",
                table: "Like",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                schema: "Fakebook",
                table: "Like",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                schema: "Fakebook",
                table: "Post",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "Follow",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "Like",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Fakebook");
        }
    }
}
