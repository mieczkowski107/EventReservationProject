using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddByteVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "SessionLimit",
                type: "RAW(8)",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Session",
                type: "RAW(8)",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Event",
                type: "RAW(8)",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "SessionLimit");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Event");
        }
    }
}
