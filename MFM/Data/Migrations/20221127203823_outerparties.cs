using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MFM.Data.Migrations
{
    public partial class outerparties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OuterPartyId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OuterPartyId",
                table: "Transactions",
                column: "OuterPartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_OuterParties_OuterPartyId",
                table: "Transactions",
                column: "OuterPartyId",
                principalTable: "OuterParties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_OuterParties_OuterPartyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OuterPartyId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "OuterPartyId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
