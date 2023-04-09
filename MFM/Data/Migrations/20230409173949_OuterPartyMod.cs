using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MFM.Data.Migrations
{
    public partial class OuterPartyMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "OuterParties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OuterParties_CategoryId",
                table: "OuterParties",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OuterParties_Categories_CategoryId",
                table: "OuterParties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OuterParties_Categories_CategoryId",
                table: "OuterParties");

            migrationBuilder.DropIndex(
                name: "IX_OuterParties_CategoryId",
                table: "OuterParties");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "OuterParties");
        }
    }
}
