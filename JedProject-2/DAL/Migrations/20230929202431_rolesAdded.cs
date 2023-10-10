using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JedProject_2.DAL.Migrations
{
    /// <inheritdoc />
    public partial class rolesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_StorageId",
                table: "Products",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Storages_StorageId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StorageId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Products");
        }
    }
}
