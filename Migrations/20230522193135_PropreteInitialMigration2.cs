using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proprete.Migrations
{
    /// <inheritdoc />
    public partial class PropreteInitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OffWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_ItemID",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Warehouse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                columns: new[] { "ItemID", "DateTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Warehouse",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "DateTime");

            migrationBuilder.CreateTable(
                name: "OffWarehouse",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationID = table.Column<int>(type: "INTEGER", nullable: false),
                    WarehouseDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffWarehouse", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OffWarehouse_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OffWarehouse_Warehouse_WarehouseDateTime",
                        column: x => x.WarehouseDateTime,
                        principalTable: "Warehouse",
                        principalColumn: "DateTime");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_ItemID",
                table: "Warehouse",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OffWarehouse_LocationID",
                table: "OffWarehouse",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_OffWarehouse_WarehouseDateTime",
                table: "OffWarehouse",
                column: "WarehouseDateTime");
        }
    }
}
