using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sora.Database.Migrations
{
    public partial class Scores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Scores",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    FileMd5 = table.Column<string>(nullable: true),
                    Count300 = table.Column<int>(nullable: false),
                    Count100 = table.Column<int>(nullable: false),
                    Count50 = table.Column<int>(nullable: false),
                    CountGeki = table.Column<int>(nullable: false),
                    CountKatu = table.Column<int>(nullable: false),
                    CountMiss = table.Column<int>(nullable: false),
                    TotalScore = table.Column<int>(nullable: false),
                    MaxCombo = table.Column<short>(nullable: false),
                    Mods = table.Column<uint>(nullable: false),
                    PlayMode = table.Column<byte>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ReplayId = table.Column<int>(nullable: true),
                    Accuracy = table.Column<double>(nullable: false),
                    PerformancePoints = table.Column<double>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        "FK_Scores_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Scores_UserId",
                "Scores",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Scores");
        }
    }
}