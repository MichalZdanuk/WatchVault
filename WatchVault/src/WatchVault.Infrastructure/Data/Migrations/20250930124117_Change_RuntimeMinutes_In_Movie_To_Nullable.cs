using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchVault.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_RuntimeMinutes_In_Movie_To_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Movie_RuntimeMinutes",
                table: "WatchListItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Movie_RuntimeMinutes",
                table: "WatchListItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
