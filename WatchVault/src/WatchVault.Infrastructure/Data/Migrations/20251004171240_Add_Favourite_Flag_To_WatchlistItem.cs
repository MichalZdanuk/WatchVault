using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchVault.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Favourite_Flag_To_WatchlistItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "WatchListItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "WatchListItems");
        }
    }
}
