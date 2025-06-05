using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventReservation.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateRepresentationInEventAndSessionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "SessionDate",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Event");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Session",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Session",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Event",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Event",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Event");

            migrationBuilder.AddColumn<string>(
                name: "EndHour",
                table: "Session",
                type: "NVARCHAR2(48)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SessionDate",
                table: "Session",
                type: "NVARCHAR2(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartHour",
                table: "Session",
                type: "NVARCHAR2(48)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Event",
                type: "NVARCHAR2(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EndHour",
                table: "Event",
                type: "NVARCHAR2(48)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Event",
                type: "NVARCHAR2(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartHour",
                table: "Event",
                type: "NVARCHAR2(48)",
                nullable: false,
                defaultValue: "");
        }
    }
}
