using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalistTierAPI.Migrations
{
    public partial class AddingApprovalForRestOfModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Topic",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Media",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Topic");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Media");
        }
    }
}
