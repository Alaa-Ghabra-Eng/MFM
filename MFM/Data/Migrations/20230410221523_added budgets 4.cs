using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MFM.Data.Migrations
{
    public partial class addedbudgets4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_CreaterUserId",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "CreaterUserId",
                table: "Budgets",
                newName: "CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_CreaterUserId",
                table: "Budgets",
                newName: "IX_Budgets_CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_CreatorUserId",
                table: "Budgets",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_CreatorUserId",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "Budgets",
                newName: "CreaterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_CreatorUserId",
                table: "Budgets",
                newName: "IX_Budgets_CreaterUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_CreaterUserId",
                table: "Budgets",
                column: "CreaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
