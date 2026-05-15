using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace apbd_cw7_git_s33418.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentManufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FoundationDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentManufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCs", x => x.Id);
                    table.CheckConstraint("CK_Product_Stock_NonNegative", "[Stock] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Code = table.Column<string>(type: "char(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentManufacturerId = table.Column<int>(type: "int", nullable: false),
                    ComponentTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Components_ComponentManufacturers_ComponentManufacturerId",
                        column: x => x.ComponentManufacturerId,
                        principalTable: "ComponentManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_ComponentTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PCComponents",
                columns: table => new
                {
                    PCId = table.Column<int>(type: "int", nullable: false),
                    ComponentCode = table.Column<string>(type: "char(10)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCComponents", x => new { x.PCId, x.ComponentCode });
                    table.ForeignKey(
                        name: "FK_PCComponents_Components_ComponentCode",
                        column: x => x.ComponentCode,
                        principalTable: "Components",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PCComponents_PCs_PCId",
                        column: x => x.PCId,
                        principalTable: "PCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComponentManufacturers",
                columns: new[] { "Id", "Abbreviation", "FoundationDate", "FullName" },
                values: new object[,]
                {
                    { 1, "INTL", new DateTime(1968, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel Corporation" },
                    { 2, "AMD", new DateTime(1969, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advanced Micro Devices, Inc." },
                    { 3, "NVDA", new DateTime(1993, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NVIDIA Corporation" },
                    { 4, "CRSR", new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Corsair Gaming, Inc." },
                    { 5, "SMSNG", new DateTime(1969, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung Electronics Co., Ltd." },
                    { 6, "ASUS", new DateTime(1989, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASUSTeK Computer Inc." },
                    { 7, "SSNC", new DateTime(1975, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sea Sonic Electronics Co., Ltd." }
                });

            migrationBuilder.InsertData(
                table: "ComponentTypes",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 1, "CPU", "Processor" },
                    { 2, "GPU", "Graphics Card" },
                    { 3, "RAM", "Memory" },
                    { 4, "SSD", "Storage Drive" },
                    { 5, "MB", "Motherboard" },
                    { 6, "PSU", "Power Supply" }
                });

            migrationBuilder.InsertData(
                table: "PCs",
                columns: new[] { "Id", "CreatedAt", "Name", "Stock", "Warranty", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "Office Mini", 18, 24, 4.2f },
                    { 2, new DateTime(2026, 2, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), "Creator Pro", 7, 36, 9.8f },
                    { 3, new DateTime(2026, 3, 20, 14, 45, 0, 0, DateTimeKind.Unspecified), "Gaming X", 11, 36, 11.4f },
                    { 4, new DateTime(2026, 4, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), "Workstation Max", 4, 48, 13.1f }
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "Code", "ComponentManufacturerId", "ComponentTypeId", "Description", "Name" },
                values: new object[,]
                {
                    { "CPU001", 1, 1, "14-core desktop processor for gaming and productivity.", "Intel Core i5-14600K" },
                    { "CPU002", 2, 1, "8-core processor with 3D V-Cache for gaming builds.", "AMD Ryzen 7 7800X3D" },
                    { "GPU001", 3, 2, "Graphics card for high-refresh 1440p gaming.", "GeForce RTX 4070 Super" },
                    { "GPU002", 2, 2, "Graphics card with 16 GB memory for gaming and creation.", "Radeon RX 7800 XT" },
                    { "MB001", 6, 5, "AM5 motherboard for Ryzen processors.", "ASUS TUF Gaming B650-Plus" },
                    { "PSU001", 7, 6, "750 W modular power supply.", "Seasonic Focus GX-750" },
                    { "RAM001", 4, 3, "Two 16 GB DDR5 memory modules.", "Corsair Vengeance 32GB DDR5" },
                    { "SSD001", 5, 4, "Fast NVMe SSD for system and project storage.", "Samsung 990 Pro 2TB" }
                });

            migrationBuilder.InsertData(
                table: "PCComponents",
                columns: new[] { "ComponentCode", "PCId", "Amount" },
                values: new object[,]
                {
                    { "CPU001", 1, 1 },
                    { "RAM001", 1, 1 },
                    { "SSD001", 1, 1 },
                    { "CPU002", 2, 1 },
                    { "GPU002", 2, 1 },
                    { "RAM001", 2, 2 },
                    { "SSD001", 2, 2 },
                    { "CPU001", 3, 1 },
                    { "GPU001", 3, 1 },
                    { "MB001", 3, 1 },
                    { "PSU001", 3, 1 },
                    { "RAM001", 3, 1 },
                    { "CPU002", 4, 1 },
                    { "GPU001", 4, 2 },
                    { "PSU001", 4, 1 },
                    { "RAM001", 4, 4 },
                    { "SSD001", 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentManufacturerId",
                table: "Components",
                column: "ComponentManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypeId",
                table: "Components",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PCComponents_ComponentCode",
                table: "PCComponents",
                column: "ComponentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PCComponents");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "PCs");

            migrationBuilder.DropTable(
                name: "ComponentManufacturers");

            migrationBuilder.DropTable(
                name: "ComponentTypes");
        }
    }
}
