using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchVault.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Genres_Column_To_Movie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Movie_Genres",
                table: "WatchListItems",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Movie_Genres",
                table: "WatchListItems");
        }
    }
}
