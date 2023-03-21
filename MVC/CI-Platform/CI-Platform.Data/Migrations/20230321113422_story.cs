using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CI_Platform.Data.Migrations
{
    /// <inheritdoc />
    public partial class story : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "short_description",
                table: "story",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "short_description",
                table: "story");
        }
    }
}
