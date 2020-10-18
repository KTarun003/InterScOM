using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace InterScOM.Migrations
{
    public partial class newvendortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorId = table.Column<int>(nullable: false),
                    VendorName = table.Column<string>(nullable: true),
                    OrderName = table.Column<string>(nullable: true),
                    OrderRefNumber = table.Column<int>(nullable: false),
                    OrderClass = table.Column<string>(nullable: true),
                    DatePlaced = table.Column<DateTime>(nullable: false),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorOrders");
        }
    }
}
