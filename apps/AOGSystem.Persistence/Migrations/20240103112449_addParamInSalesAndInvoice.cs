using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addParamInSalesAndInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_invoice",
                schema: "AOGsystem",
                table: "sales_part_list",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "awb_no",
                schema: "AOGsystem",
                table: "sales",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "is_approve",
                schema: "AOGsystem",
                table: "sales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_fully_shipped",
                schema: "AOGsystem",
                table: "sales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "received_bu_customer",
                schema: "AOGsystem",
                table: "sales",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "received_date",
                schema: "AOGsystem",
                table: "sales",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ship_date",
                schema: "AOGsystem",
                table: "sales",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoicePartList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UOM = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RID = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAT = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePartList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoicePartList_invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "AOGsystem",
                        principalTable: "invoice",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePartList_InvoiceId",
                table: "InvoicePartList",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePartList");

            migrationBuilder.DropColumn(
                name: "is_invoice",
                schema: "AOGsystem",
                table: "sales_part_list");

            migrationBuilder.DropColumn(
                name: "awb_no",
                schema: "AOGsystem",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "is_approve",
                schema: "AOGsystem",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "is_fully_shipped",
                schema: "AOGsystem",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "received_bu_customer",
                schema: "AOGsystem",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "received_date",
                schema: "AOGsystem",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "ship_date",
                schema: "AOGsystem",
                table: "sales");
        }
    }
}
