using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocks.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Stocks");

            migrationBuilder.CreateSequence(
                name: "AccountsSequence",
                schema: "Stocks",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "StockBalancesSequence",
                schema: "Stocks",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "TransactionsSequence",
                schema: "Stocks",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                schema: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockBalances",
                schema: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shares = table.Column<int>(type: "int", nullable: false),
                    SharePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockBalances_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Stocks",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: true),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shares = table.Column<int>(type: "int", nullable: false),
                    SharePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Operation_OperationId",
                        column: x => x.OperationId,
                        principalSchema: "Stocks",
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Stocks",
                table: "Operation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Buy" });

            migrationBuilder.InsertData(
                schema: "Stocks",
                table: "Operation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Sell" });

            migrationBuilder.CreateIndex(
                name: "IX_StockBalances_AccountId",
                schema: "Stocks",
                table: "StockBalances",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OperationId",
                schema: "Stocks",
                table: "Transactions",
                column: "OperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockBalances",
                schema: "Stocks");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "Stocks");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "Stocks");

            migrationBuilder.DropTable(
                name: "Operation",
                schema: "Stocks");

            migrationBuilder.DropSequence(
                name: "AccountsSequence",
                schema: "Stocks");

            migrationBuilder.DropSequence(
                name: "StockBalancesSequence",
                schema: "Stocks");

            migrationBuilder.DropSequence(
                name: "TransactionsSequence",
                schema: "Stocks");
        }
    }
}
