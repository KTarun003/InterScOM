using Microsoft.EntityFrameworkCore.Migrations;

namespace InterScOM.Migrations
{
    public partial class AddedAppIdFKToFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Fee_ApplicationId",
                table: "Fee",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Application_ApplicationId",
                table: "Fee",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Application_ApplicationId",
                table: "Fee");

            migrationBuilder.DropIndex(
                name: "IX_Fee_ApplicationId",
                table: "Fee");
        }
    }
}
