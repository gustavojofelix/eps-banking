using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cnet_oykryo.web.api.Migrations
{
    public partial class newSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "CustomerId" },
                values: new object[,]
                {
                    { 1, "123456", 2000m, 1 },
                    { 2, "65231", 2500m, 2 },
                    { 3, "8523", 2900m, 3 },
                    { 4, "69874", 2500m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Transfers",
                columns: new[] { "Id", "Amount", "DestinationAccountId", "SourceAccountId", "TransferDate" },
                values: new object[,]
                {
                    { 1, 10m, 1, 4, new DateTime(2024, 1, 30, 13, 24, 8, 552, DateTimeKind.Local).AddTicks(8885) },
                    { 2, 50m, 2, 3, new DateTime(2024, 1, 30, 13, 24, 8, 552, DateTimeKind.Local).AddTicks(8898) },
                    { 3, 100m, 3, 2, new DateTime(2024, 1, 30, 13, 24, 8, 552, DateTimeKind.Local).AddTicks(8925) },
                    { 4, 75m, 4, 1, new DateTime(2024, 1, 30, 13, 24, 8, 552, DateTimeKind.Local).AddTicks(8926) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_DestinationAccountId",
                table: "Transfers",
                column: "DestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SourceAccountId",
                table: "Transfers",
                column: "SourceAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_BankAccounts_DestinationAccountId",
                table: "Transfers",
                column: "DestinationAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_BankAccounts_SourceAccountId",
                table: "Transfers",
                column: "SourceAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_BankAccounts_DestinationAccountId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_BankAccounts_SourceAccountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_DestinationAccountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_SourceAccountId",
                table: "Transfers");

            migrationBuilder.DeleteData(
                table: "Transfers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transfers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transfers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transfers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
