using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NavigationToSessionLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionLimit_SessionId",
                table: "SessionLimit");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLimit_SessionId",
                table: "SessionLimit",
                column: "SessionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SessionLimit_SessionId",
                table: "SessionLimit");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLimit_SessionId",
                table: "SessionLimit",
                column: "SessionId");
        }
    }
}
