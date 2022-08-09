using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addedVirtual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "07e0c251-8262-464a-b9ac-965d97f36c69");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "4eac20b6-a49e-499b-95d2-7c8c26232f6e");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "77e433a3-9484-4475-b4eb-90e440e1ebab");

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[] { "531515b6-b1dd-406a-95ca-2d00387124f2", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2021, 3, 18, 0, 6, 35, 52, DateTimeKind.Local).AddTicks(3813), 1 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[] { "bb1b5fda-d345-45fa-b711-f3ab5370de83", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2022, 3, 19, 0, 6, 35, 52, DateTimeKind.Local).AddTicks(3786), 0 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[] { "eada2808-d48d-4fa1-a783-72bce6be7593", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2020, 3, 18, 0, 6, 35, 52, DateTimeKind.Local).AddTicks(3817), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "531515b6-b1dd-406a-95ca-2d00387124f2");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "bb1b5fda-d345-45fa-b711-f3ab5370de83");

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: "eada2808-d48d-4fa1-a783-72bce6be7593");

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[] { "07e0c251-8262-464a-b9ac-965d97f36c69", "37846734-172e-4149-8cec-6f43d1eb3f60", 1000m, new DateTime(2020, 3, 17, 23, 56, 8, 965, DateTimeKind.Local).AddTicks(2946), 0 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[] { "4eac20b6-a49e-499b-95d2-7c8c26232f6e", "37846734-172e-4149-8cec-6f43d1eb3f60", -500m, new DateTime(2021, 3, 17, 23, 56, 8, 965, DateTimeKind.Local).AddTicks(2935), 1 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "TransactionAmount", "TransactionDate", "TransactionType" },
                values: new object[] { "77e433a3-9484-4475-b4eb-90e440e1ebab", "37846734-172e-4149-8cec-6f43d1eb3f60", 3000m, new DateTime(2022, 3, 18, 23, 56, 8, 965, DateTimeKind.Local).AddTicks(2896), 0 });
        }
    }
}
