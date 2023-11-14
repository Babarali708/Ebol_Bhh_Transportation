using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Transportation.Migrations
{
    /// <inheritdoc />
    public partial class initial_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(355)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Contact", "CreatedAt", "CreatedBy", "DeletedAt", "Email", "FirstName", "IsActive", "LastName", "ModifiedAt", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "00000000000", new DateTime(2023, 9, 20, 16, 33, 4, 47, DateTimeKind.Local).AddTicks(8914), null, null, "superadmin@yopmail.com", "Super", 1, "Admin", null, "123", 0 },
                    { 2, "00000000000", new DateTime(2023, 9, 20, 16, 33, 4, 47, DateTimeKind.Local).AddTicks(8938), null, null, "admin@yopmail.com", "Admin", 1, "Admin", null, "123", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
