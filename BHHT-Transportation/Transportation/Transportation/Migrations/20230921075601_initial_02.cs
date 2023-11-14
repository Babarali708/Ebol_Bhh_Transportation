using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.Migrations
{
    /// <inheritdoc />
    public partial class initial_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransporterRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    AttendentId = table.Column<int>(type: "int", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarnFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromCityAndState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarnTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToCityAndState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransporterRecord", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 21, 12, 56, 1, 340, DateTimeKind.Local).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 21, 12, 56, 1, 340, DateTimeKind.Local).AddTicks(7566));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransporterRecord");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 20, 16, 33, 4, 47, DateTimeKind.Local).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 20, 16, 33, 4, 47, DateTimeKind.Local).AddTicks(8938));
        }
    }
}
