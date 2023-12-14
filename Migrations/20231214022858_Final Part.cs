using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_EnterpriseSystem.Migrations
{
    /// <inheritdoc />
    public partial class FinalPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Songs",
                newName: "ArtistObjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                newName: "IX_Songs_ArtistObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistObjectId",
                table: "Songs",
                column: "ArtistObjectId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistObjectId",
                table: "Songs");

            migrationBuilder.RenameColumn(
                name: "ArtistObjectId",
                table: "Songs",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_ArtistObjectId",
                table: "Songs",
                newName: "IX_Songs_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
