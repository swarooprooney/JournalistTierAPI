using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalistTierAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journalist",
                columns: table => new
                {
                    JournalistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journalist", x => x.JournalistId);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    MediaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.MediaId);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserJournalistRating",
                columns: table => new
                {
                    UserJournalistRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalistId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJournalistRating", x => x.UserJournalistRatingId);
                    table.ForeignKey(
                        name: "FK_UserJournalistRating_Journalist_JournalistId",
                        column: x => x.JournalistId,
                        principalTable: "Journalist",
                        principalColumn: "JournalistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserJournalistRating_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserJournalistRating_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMediaRating",
                columns: table => new
                {
                    UserMediaRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    MediaId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMediaRating", x => x.UserMediaRatingId);
                    table.ForeignKey(
                        name: "FK_UserMediaRating_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMediaRating_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMediaRating_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserJournalistRating_JournalistId",
                table: "UserJournalistRating",
                column: "JournalistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJournalistRating_TopicId",
                table: "UserJournalistRating",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJournalistRating_UserId",
                table: "UserJournalistRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMediaRating_MediaId",
                table: "UserMediaRating",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMediaRating_TopicId",
                table: "UserMediaRating",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMediaRating_UserId",
                table: "UserMediaRating",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserJournalistRating");

            migrationBuilder.DropTable(
                name: "UserMediaRating");

            migrationBuilder.DropTable(
                name: "Journalist");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
