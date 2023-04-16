using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MFM.Data.Migrations
{
    public partial class TransactionBudget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIncome",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "budgetId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_budgetId",
                table: "Transactions",
                column: "budgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Budgets_budgetId",
                table: "Transactions",
                column: "budgetId",
                principalTable: "Budgets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Budgets_budgetId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_budgetId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsIncome",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "budgetId",
                table: "Transactions");
        }
    }
}
