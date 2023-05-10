using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Data.Migrations
{
    public partial class FK3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__users__notificationsetting_i__4AEE069C",
                table: "NotificationSettings");

            migrationBuilder.DropIndex(
                name: "IX_NotificationSettings_UserId",
                table: "NotificationSettings");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_UserId",
                table: "NotificationSettings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationSettings_users_UserId",
                table: "NotificationSettings",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationSettings_users_UserId",
                table: "NotificationSettings");

            migrationBuilder.DropIndex(
                name: "IX_NotificationSettings_UserId",
                table: "NotificationSettings");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_UserId",
                table: "NotificationSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__users__notificationsetting_i__4AEE069C",
                table: "NotificationSettings",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id");
        }
    }
}
