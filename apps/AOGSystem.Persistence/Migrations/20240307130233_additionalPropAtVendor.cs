using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class additionalPropAtVendor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "soa_handler_buyer_id",
                schema: "AOGsystem",
                table: "vendor",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "soa_handler_buyer_name",
                schema: "AOGsystem",
                table: "vendor",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "soa_handler_buyer_id",
                schema: "AOGsystem",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "soa_handler_buyer_name",
                schema: "AOGsystem",
                table: "vendor");
        }
    }
}
