using Microsoft.EntityFrameworkCore.Migrations;

namespace InterScOM.Migrations
{
    public partial class AddedFeesToApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fees",
                table: "Application",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fees",
                table: "Application");
        }
    }
}
