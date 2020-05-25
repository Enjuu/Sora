using Microsoft.EntityFrameworkCore.Migrations;

namespace Sora.Database.Migrations
{
    public partial class Achievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Achievements",
                "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                "Achievements",
                table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IconURI = table.Column<string>(nullable: false),
                },
                constraints: table => { table.PrimaryKey("PK_Achievements", x => x.Name); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Achievements");

            migrationBuilder.DropColumn(
                "Achievements",
                "Users");
        }
    }
}