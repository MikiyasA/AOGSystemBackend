using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class releasedDateAndShipToAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ship_to_address",
                schema: "AOGsystem",
                table: "loans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                schema: "AOGsystem",
                table: "invoice_part_list",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "part_released_date",
                schema: "AOGsystem",
                table: "core_follow_ups",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ship_to_address",
                schema: "AOGsystem",
                table: "loans");

            migrationBuilder.DropColumn(
                name: "part_released_date",
                schema: "AOGsystem",
                table: "core_follow_ups");

            migrationBuilder.UpdateData(
                schema: "AOGsystem",
                table: "invoice_part_list",
                keyColumn: "currency",
                keyValue: null,
                column: "currency",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "currency",
                schema: "AOGsystem",
                table: "invoice_part_list",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
