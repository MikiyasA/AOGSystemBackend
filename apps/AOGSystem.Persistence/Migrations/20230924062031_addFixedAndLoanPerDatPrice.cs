using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addFixedAndLoanPerDatPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "loan_price",
                schema: "AOGsystem",
                table: "quotation_partLists",
                newName: "loan_price_per_day");

            migrationBuilder.AddColumn<decimal>(
                name: "fixed_loan_price",
                schema: "AOGsystem",
                table: "quotation_partLists",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fixed_loan_price",
                schema: "AOGsystem",
                table: "quotation_partLists");

            migrationBuilder.RenameColumn(
                name: "loan_price_per_day",
                schema: "AOGsystem",
                table: "quotation_partLists",
                newName: "loan_price");
        }
    }
}
