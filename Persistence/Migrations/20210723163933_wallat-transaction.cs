using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class wallattransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayerId = table.Column<int>(type: "int", nullable: false),
                    PayeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_WallatTransactions_PayeeId",
                        column: x => x.PayeeId,
                        principalTable: "WallatTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_WallatTransactions_PayerId",
                        column: x => x.PayerId,
                        principalTable: "WallatTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PayeeId",
                table: "Transactions",
                column: "PayeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PayerId",
                table: "Transactions",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallats_UserId",
                table: "Wallats",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WallatTransactions_WallatId",
                table: "WallatTransactions",
                column: "WallatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "WallatTransactions");

            migrationBuilder.DropTable(
                name: "Wallats");
        }
    }
}
