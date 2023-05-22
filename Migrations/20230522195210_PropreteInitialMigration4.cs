using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proprete.Migrations
{
    /// <inheritdoc />
    public partial class PropreteInitialMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_ItemID_DateTime",
                table: "Warehouse",
                columns: new[] { "ItemID", "DateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_OffWarehouse_LocationID_ItemID_DateTime",
                table: "OffWarehouse",
                columns: new[] { "LocationID", "ItemID", "DateTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warehouse_ItemID_DateTime",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_OffWarehouse_LocationID_ItemID_DateTime",
                table: "OffWarehouse");
        }
    }
}
