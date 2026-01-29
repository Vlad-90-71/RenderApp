using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenderApp.Migrations
{
    /// <inheritdoc />
    public partial class Dropbox1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Users",
                type: "bytea",
                nullable: true);
        }
    }
}
