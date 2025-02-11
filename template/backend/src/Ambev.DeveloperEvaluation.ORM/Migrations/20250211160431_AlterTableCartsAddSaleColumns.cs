using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableCartsAddSaleColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Carts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Carts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Carts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BranchId",
                table: "Carts",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Branches_BranchId",
                table: "Carts",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Branches_BranchId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_BranchId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Carts");
        }
    }
}
