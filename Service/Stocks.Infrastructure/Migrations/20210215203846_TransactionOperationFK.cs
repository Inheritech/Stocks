using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocks.Infrastructure.Migrations
{
    public partial class TransactionOperationFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Operation_OperationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OperationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OperationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "_operationId",
                schema: "Stocks",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions__operationId",
                schema: "Stocks",
                table: "Transactions",
                column: "_operationId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Operation__operationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions__operationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "_operationId",
                schema: "Stocks",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "OperationId",
                schema: "Stocks",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OperationId",
                schema: "Stocks",
                table: "Transactions",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Operation_OperationId",
                schema: "Stocks",
                table: "Transactions",
                column: "OperationId",
                principalSchema: "Stocks",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
