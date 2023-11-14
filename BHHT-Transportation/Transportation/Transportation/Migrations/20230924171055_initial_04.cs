using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transportation.Migrations
{
    /// <inheritdoc />
    public partial class initial_04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiviedOrderRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransporterRecordId = table.Column<int>(type: "int", nullable: true),
                    ReceivingDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Check = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicCharges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargesForExcessValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Layovers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VetFees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCharges = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierPerAgentOrDriver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiviedOrderRecord", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 24, 22, 10, 55, 422, DateTimeKind.Local).AddTicks(1974));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 24, 22, 10, 55, 422, DateTimeKind.Local).AddTicks(1991));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiviedOrderRecord");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 22, 11, 56, 5, 120, DateTimeKind.Local).AddTicks(9039));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 22, 11, 56, 5, 120, DateTimeKind.Local).AddTicks(9056));
        }
    }
}
