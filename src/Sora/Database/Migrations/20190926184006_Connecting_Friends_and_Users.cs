using Microsoft.EntityFrameworkCore.Migrations;

namespace Sora.Database.Migrations
{
    public partial class ConnectingFriendsAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "UserName",
                "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Permissions",
                "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Password",
                "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "EMail",
                "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                "IX_Friends_FriendId",
                "Friends",
                "FriendId");

            migrationBuilder.CreateIndex(
                "IX_Friends_UserId",
                "Friends",
                "UserId");

            migrationBuilder.AddForeignKey(
                "FK_Friends_Users_FriendId",
                "Friends",
                "FriendId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Friends_Users_UserId",
                "Friends",
                "UserId",
                "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Friends_Users_FriendId",
                "Friends");

            migrationBuilder.DropForeignKey(
                "FK_Friends_Users_UserId",
                "Friends");

            migrationBuilder.DropIndex(
                "IX_Friends_FriendId",
                "Friends");

            migrationBuilder.DropIndex(
                "IX_Friends_UserId",
                "Friends");

            migrationBuilder.AlterColumn<string>(
                "UserName",
                "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Permissions",
                "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Password",
                "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "EMail",
                "Users",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}