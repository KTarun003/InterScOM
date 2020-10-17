﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace InterScOM.Migrations.Identity
{
    public partial class AddedAppIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppId",
                table: "AspNetUsers");
        }
    }
}
