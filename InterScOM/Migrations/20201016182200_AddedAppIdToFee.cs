using Microsoft.EntityFrameworkCore.Migrations;

namespace InterScOM.Migrations
{
    public partial class AddedAppIdToFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Fee",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Fee");
        }
    }
}
