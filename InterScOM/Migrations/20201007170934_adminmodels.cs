using Microsoft.EntityFrameworkCore.Migrations;

namespace InterScOM.Migrations
{
    public partial class adminmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Itemname = table.Column<string>(nullable: true),
                    ItemClass = table.Column<string>(nullable: true),
                    ItemQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Supplies");
        }
    }
}
