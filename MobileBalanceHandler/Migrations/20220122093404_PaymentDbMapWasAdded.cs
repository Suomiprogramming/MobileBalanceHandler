using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBalanceHandler.Migrations
{
    public partial class PaymentDbMapWasAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Sum",
                table: "Payments",
                type: "numeric(7,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 22, 15, 34, 4, 529, DateTimeKind.Local).AddTicks(4398),
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Sum",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(7,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2022, 1, 22, 15, 34, 4, 529, DateTimeKind.Local).AddTicks(4398));
        }
    }
}
