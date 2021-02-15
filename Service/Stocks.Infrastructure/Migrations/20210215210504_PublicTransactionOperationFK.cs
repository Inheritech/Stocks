using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocks.Infrastructure.Migrations
{
    public partial class PublicTransactionOperationFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Operation__operationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "_operationId",
                schema: "Stocks",
                table: "Transactions",
                newName: "OperationId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions__operationId",
                schema: "Stocks",
                table: "Transactions",
                newName: "IX_Transactions_OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Operation_OperationId",
                schema: "Stocks",
                table: "Transactions",
                column: "OperationId",
                principalSchema: "Stocks",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Operation_OperationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "OperationId",
                schema: "Stocks",
                table: "Transactions",
                newName: "_operationId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OperationId",
                schema: "Stocks",
                table: "Transactions",
                newName: "IX_Transactions__operationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Operation__operationId",
                schema: "Stocks",
                table: "Transactions",
                column: "_operationId",
                principalSchema: "Stocks",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
