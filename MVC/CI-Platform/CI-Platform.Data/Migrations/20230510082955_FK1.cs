using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Data.Migrations
{
    public partial class FK1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationSettings_users_UserId",
                table: "NotificationSettings");

            migrationBuilder.AddForeignKey(
                name: "FK__users__notificationsetting_i__4AEE069C",
                table: "NotificationSettings",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__users__notificationsetting_i__4AEE069C",
                table: "NotificationSettings");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationSettings_users_UserId",
                table: "NotificationSettings",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
