using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MFM.Data.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "InnerPartyId",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transaction",
                newName: "IX_Transaction_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "CreaterUserId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Transaction",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreaterUserId",
                table: "Categories",
                column: "CreaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CreatorUserId",
                table: "Transaction",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreaterUserId",
                table: "Categories",
                column: "CreaterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_CreatorUserId",
                table: "Transaction",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Categories_CategoryId",
                table: "Transaction",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreaterUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_CreatorUserId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Categories_CategoryId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreaterUserId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CreatorUserId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreaterUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CategoryId",
                table: "Transactions",
                newName: "IX_Transactions_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "InnerPartyId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
