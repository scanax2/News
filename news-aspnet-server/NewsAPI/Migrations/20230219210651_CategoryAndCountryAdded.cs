using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetServer.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAndCountryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "News");
        }
    }
}
