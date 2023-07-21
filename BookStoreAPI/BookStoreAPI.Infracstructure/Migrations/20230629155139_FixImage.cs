using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Infracstructure.Migrations
{
    /// <inheritdoc />
    public partial class FixImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Name",
                table: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_Name",
                table: "Image",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
