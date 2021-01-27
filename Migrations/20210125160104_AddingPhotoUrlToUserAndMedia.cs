using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalistTierAPI.Migrations
{
    public partial class AddingPhotoUrlToUserAndMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Media",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Media");
        }
    }
}
