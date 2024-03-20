using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class nameErrorFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLists_vendor_VendorId",
                table: "InvoiceLists");

            migrationBuilder.RenameColumn(
                name: "UnderForllowup",
                table: "InvoiceLists",
                newName: "UnderFollowup");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendorId",
                table: "InvoiceLists",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLists_vendor_VendorId",
                table: "InvoiceLists",
                column: "VendorId",
                principalSchema: "AOGsystem",
                principalTable: "vendor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLists_vendor_VendorId",
                table: "InvoiceLists");

            migrationBuilder.RenameColumn(
                name: "UnderFollowup",
                table: "InvoiceLists",
                newName: "UnderForllowup");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendorId",
                table: "InvoiceLists",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLists_vendor_VendorId",
                table: "InvoiceLists",
                column: "VendorId",
                principalSchema: "AOGsystem",
                principalTable: "vendor",
                principalColumn: "id");
        }
    }
}
