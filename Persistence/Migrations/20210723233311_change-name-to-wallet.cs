using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class changenametowallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WallatTransactions_PayeeId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WallatTransactions_PayerId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "WallatTransactions");

            migrationBuilder.DropTable(
                name: "Wallats");

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WalletTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    WalletId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletTransactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletTransactions_WalletId",
                table: "WalletTransactions",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WalletTransactions_PayeeId",
                table: "Transactions",
                column: "PayeeId",
                principalTable: "WalletTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WalletTransactions_PayerId",
                table: "Transactions",
                column: "PayerId",
                principalTable: "WalletTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WalletTransactions_PayeeId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_WalletTransactions_PayerId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "WalletTransactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.CreateTable(
                name: "Wallats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WallatTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    WallatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WallatTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WallatTransactions_Wallats_WallatId",
                        column: x => x.WallatId,
                        principalTable: "Wallats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallats_UserId",
                table: "Wallats",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WallatTransactions_WallatId",
                table: "WallatTransactions",
                column: "WallatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WallatTransactions_PayeeId",
                table: "Transactions",
                column: "PayeeId",
                principalTable: "WallatTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_WallatTransactions_PayerId",
                table: "Transactions",
                column: "PayerId",
                principalTable: "WallatTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
