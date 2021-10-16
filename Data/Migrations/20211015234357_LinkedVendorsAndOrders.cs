using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class LinkedVendorsAndOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderClass",
                table: "VendorOrders");

            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "VendorOrders");

            migrationBuilder.DropColumn(
                name: "OrderRefNumber",
                table: "VendorOrders");

            migrationBuilder.DropColumn(
                name: "VendorName",
                table: "VendorOrders");

            migrationBuilder.DropColumn(
                name: "ItemClass",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Itemname",
                table: "Supplies");

            migrationBuilder.RenameColumn(
                name: "Ordersplaced",
                table: "Vendor",
                newName: "OrdersPlaced");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "VendorOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Supplies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceId",
                table: "Supplies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Supplies",
                nullable: true);

            migrationBuilder.AddForeignKey(name: "FK_VendorOrders_Vendor_VendorId",
                table: "VendorOrders",
                column: "OrderId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_VendorOrders_Supplies_OrderId",
                table: "VendorOrders",
                column: "OrderId",
                principalTable: "Supplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade,
                onUpdate: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "VendorOrders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "Supplies");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Supplies");

            migrationBuilder.RenameColumn(
                name: "OrdersPlaced",
                table: "Vendor",
                newName: "Ordersplaced");

            migrationBuilder.AddColumn<string>(
                name: "OrderClass",
                table: "VendorOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "VendorOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderRefNumber",
                table: "VendorOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VendorName",
                table: "VendorOrders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemClass",
                table: "Supplies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Itemname",
                table: "Supplies",
                type: "text",
                nullable: true);
        }
    }
}
