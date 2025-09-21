using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchVault.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_WatchList_And_WatchListItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchListItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WatchListId = table.Column<Guid>(type: "uuid", nullable: false),
                    WatchStatus = table.Column<int>(type: "integer", nullable: false),
                    AddedToWatchAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WatchedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Movie_Director = table.Column<string>(type: "text", nullable: false),
                    Movie_Overview = table.Column<string>(type: "text", nullable: false),
                    Movie_PosterUrl = table.Column<string>(type: "text", nullable: false),
                    Movie_ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Movie_RuntimeMinutes = table.Column<int>(type: "integer", nullable: false),
                    Movie_SimklId = table.Column<int>(type: "integer", nullable: false),
                    Movie_Title = table.Column<string>(type: "text", nullable: false),
                    Movie_Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchListItems_WatchLists_WatchListId",
                        column: x => x.WatchListId,
                        principalTable: "WatchLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchListItems_WatchListId",
                table: "WatchListItems",
                column: "WatchListId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLists_UserId",
                table: "WatchLists",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchListItems");

            migrationBuilder.DropTable(
                name: "WatchLists");
        }
    }
}
