using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sora.Database.Migrations
{
    public partial class StatusFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "Status",
                "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                "StatusReason",
                "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                "StatusUntil",
                "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Status",
                "Users");

            migrationBuilder.DropColumn(
                "StatusReason",
                "Users");

            migrationBuilder.DropColumn(
                "StatusUntil",
                "Users");
        }
    }
}