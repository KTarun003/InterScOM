using Microsoft.EntityFrameworkCore.Migrations;

namespace InterScOM.Migrations
{
    public partial class AddedVotesToAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownVotes",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVotes",
                table: "Answers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVotes",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "UpVotes",
                table: "Answers");
        }
    }
}
