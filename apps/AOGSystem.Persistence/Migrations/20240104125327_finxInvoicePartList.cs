using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class finxInvoicePartList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicePartList_invoice_InvoiceId",
                table: "InvoicePartList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoicePartList",
                table: "InvoicePartList");

            migrationBuilder.RenameTable(
                name: "InvoicePartList",
                newName: "invoice_part_list",
                newSchema: "AOGsystem");

            migrationBuilder.RenameColumn(
                name: "UOM",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "uom");

            migrationBuilder.RenameColumn(
                name: "RID",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "rid");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Currency",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "UpdatedAT",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "total_price");

            migrationBuilder.RenameColumn(
                name: "SerialNo",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "serila_no");

            migrationBuilder.RenameColumn(
                name: "PartId",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "part_id");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CreatedAT",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicePartList_InvoiceId",
                schema: "AOGsystem",
                table: "invoice_part_list",
                newName: "IX_invoice_part_list_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_invoice_part_list",
                schema: "AOGsystem",
                table: "invoice_part_list",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_part_list_part_id",
                schema: "AOGsystem",
                table: "invoice_part_list",
                column: "part_id");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_part_list_invoice_InvoiceId",
                schema: "AOGsystem",
                table: "invoice_part_list",
                column: "InvoiceId",
                principalSchema: "AOGsystem",
                principalTable: "invoice",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_part_list_parts_part_id",
                schema: "AOGsystem",
                table: "invoice_part_list",
                column: "part_id",
                principalSchema: "AOGsystem",
                principalTable: "parts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_part_list_invoice_InvoiceId",
                schema: "AOGsystem",
                table: "invoice_part_list");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_part_list_parts_part_id",
                schema: "AOGsystem",
                table: "invoice_part_list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_invoice_part_list",
                schema: "AOGsystem",
                table: "invoice_part_list");

            migrationBuilder.DropIndex(
                name: "IX_invoice_part_list_part_id",
                schema: "AOGsystem",
                table: "invoice_part_list");

            migrationBuilder.RenameTable(
                name: "invoice_part_list",
                schema: "AOGsystem",
                newName: "InvoicePartList");

            migrationBuilder.RenameColumn(
                name: "uom",
                table: "InvoicePartList",
                newName: "UOM");

            migrationBuilder.RenameColumn(
                name: "rid",
                table: "InvoicePartList",
                newName: "RID");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "InvoicePartList",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "InvoicePartList",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "InvoicePartList",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "InvoicePartList",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "InvoicePartList",
                newName: "UpdatedAT");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "InvoicePartList",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "InvoicePartList",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "serila_no",
                table: "InvoicePartList",
                newName: "SerialNo");

            migrationBuilder.RenameColumn(
                name: "part_id",
                table: "InvoicePartList",
                newName: "PartId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "InvoicePartList",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "InvoicePartList",
                newName: "CreatedAT");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_part_list_InvoiceId",
                table: "InvoicePartList",
                newName: "IX_InvoicePartList_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoicePartList",
                table: "InvoicePartList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicePartList_invoice_InvoiceId",
                table: "InvoicePartList",
                column: "InvoiceId",
                principalSchema: "AOGsystem",
                principalTable: "invoice",
                principalColumn: "id");
        }
    }
}
