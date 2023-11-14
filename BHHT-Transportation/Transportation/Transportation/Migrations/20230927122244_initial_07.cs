using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.Migrations
{
    /// <inheritdoc />
    public partial class initial_07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TractorDriver",
                table: "TransporterRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TractorNumber",
                table: "TransporterRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerDriver",
                table: "TransporterRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerNumber",
                table: "TransporterRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 27, 17, 22, 44, 717, DateTimeKind.Local).AddTicks(752));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 27, 17, 22, 44, 717, DateTimeKind.Local).AddTicks(765));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TractorDriver",
                table: "TransporterRecord");

            migrationBuilder.DropColumn(
                name: "TractorNumber",
                table: "TransporterRecord");

            migrationBuilder.DropColumn(
                name: "TrailerDriver",
                table: "TransporterRecord");

            migrationBuilder.DropColumn(
                name: "TrailerNumber",
                table: "TransporterRecord");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 25, 16, 46, 56, 410, DateTimeKind.Local).AddTicks(6560));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 25, 16, 46, 56, 410, DateTimeKind.Local).AddTicks(6576));
        }
    }
}
