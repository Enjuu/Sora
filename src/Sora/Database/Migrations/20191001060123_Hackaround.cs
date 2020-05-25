using Microsoft.EntityFrameworkCore.Migrations;

namespace Sora.Database.Migrations
{
    public partial class Hackaround : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Leaderboard_Users_Id",
                "Leaderboard");

            migrationBuilder.DropColumn(
                "ReplayId",
                "Scores");

            migrationBuilder.AddColumn<int>(
                "OwnerId",
                "Leaderboard",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_Leaderboard_OwnerId",
                "Leaderboard",
                "OwnerId");

            migrationBuilder.AddForeignKey(
                "FK_Leaderboard_Users_OwnerId",
                "Leaderboard",
                "OwnerId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Leaderboard_Users_OwnerId",
                "Leaderboard");

            migrationBuilder.DropIndex(
                "IX_Leaderboard_OwnerId",
                "Leaderboard");

            migrationBuilder.DropColumn(
                "OwnerId",
                "Leaderboard");

            migrationBuilder.AddColumn<int>(
                "ReplayId",
                "Scores",
                nullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Leaderboard_Users_Id",
                "Leaderboard",
                "Id",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}