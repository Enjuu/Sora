using Microsoft.EntityFrameworkCore.Migrations;

namespace Sora.Database.Migrations
{
    public partial class OptimizeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "UserName",
                "Users",
                "varchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                "EMail",
                "Users",
                "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                "ReplayMd5",
                "Scores",
                "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "FileMd5",
                "Scores",
                "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Beatmaps",
                "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                "FileName",
                "Beatmaps",
                "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                "FileMd5",
                "Beatmaps",
                "varchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                "DiffName",
                "Beatmaps",
                "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                "Artist",
                "Beatmaps",
                "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.CreateIndex("IX_Beatmaps_SetId", "Beatmaps", "SetId", unique: false);
            migrationBuilder.CreateIndex("IX_Beatmaps_FileMd5", "Beatmaps", "FileMd5", unique: true);
            migrationBuilder.CreateIndex("IX_Beatmaps_RankedStatus", "Beatmaps", "RankedStatus", unique: false);

            migrationBuilder.CreateIndex("IX_Scores_FileMd5", "Scores", "FileMd5", unique: false);
            migrationBuilder.CreateIndex("IX_Scores_Mods", "Scores", "Mods", unique: false);
            migrationBuilder.CreateIndex("IX_Scores_PlayMode", "Scores", "PlayMode", unique: false);
            migrationBuilder.CreateIndex("IX_Scores_ReplayMd5", "Scores", "ReplayMd5", unique: true);

            migrationBuilder.CreateIndex("IX_Users_UserName", "Users", "UserName", unique: true);
            migrationBuilder.CreateIndex("IX_Users_EMail", "Users", "EMail", unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "UserName",
                "Users",
                "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)");

            migrationBuilder.AlterColumn<string>(
                "EMail",
                "Users",
                "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AlterColumn<string>(
                "ReplayMd5",
                "Scores",
                "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Beatmaps",
                "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                "FileName",
                "Beatmaps",
                "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                "FileMd5",
                "Beatmaps",
                "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)");

            migrationBuilder.AlterColumn<string>(
                "DiffName",
                "Beatmaps",
                "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                "Artist",
                "Beatmaps",
                "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.DropIndex("IX_Beatmaps_SetId");
            migrationBuilder.DropIndex("IX_Beatmaps_FileMd5");
            migrationBuilder.DropIndex("IX_Beatmaps_RankedStatus");

            migrationBuilder.DropIndex("IX_Scores_FileMd5");
            migrationBuilder.DropIndex("IX_Scores_Mods");
            migrationBuilder.DropIndex("IX_Scores_PlayMode");
            migrationBuilder.DropIndex("IX_Scores_ReplayMd5");

            migrationBuilder.DropIndex("IX_Users_UserName");
            migrationBuilder.DropIndex("IX_Users_EMail");
        }
    }
}