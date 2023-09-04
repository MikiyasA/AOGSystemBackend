using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fromStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AOGsystem");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "quotations",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    loan = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    sales = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    exchange = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    requested_by_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requested_by_email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requested_by_phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotations", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    city = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ship_to_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bill_to_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payment_term = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.id);
                    table.ForeignKey(
                        name: "FK_companies_quotations_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "AOGsystem",
                        principalTable: "quotations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "home_base_follow_ups",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rid = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    request_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    air_craft = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tail_no = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    customer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    po_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    order_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    uom = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    vendor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    esd = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    need_higher_mgnt_attn = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RemarkId = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_home_base_follow_ups", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "remarks",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    message = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBaseFollowUpId = table.Column<int>(type: "int", nullable: true),
                    RemarkId = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_remarks", x => x.id);
                    table.ForeignKey(
                        name: "FK_remarks_home_base_follow_ups_HomeBaseFollowUpId",
                        column: x => x.HomeBaseFollowUpId,
                        principalSchema: "AOGsystem",
                        principalTable: "home_base_follow_ups",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_remarks_home_base_follow_ups_RemarkId",
                        column: x => x.RemarkId,
                        principalSchema: "AOGsystem",
                        principalTable: "home_base_follow_ups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "parts",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    part_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stock_no = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    financial_class = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HomeBaseFollowUpId = table.Column<int>(type: "int", nullable: true),
                    QuotationPartListId = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parts", x => x.id);
                    table.ForeignKey(
                        name: "FK_parts_home_base_follow_ups_HomeBaseFollowUpId",
                        column: x => x.HomeBaseFollowUpId,
                        principalSchema: "AOGsystem",
                        principalTable: "home_base_follow_ups",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "quotation_partLists",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    QuotationId = table.Column<int>(type: "int", nullable: false),
                    current_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    sales_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    loan_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    exchange_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    stock_location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    condition = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updated_by = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotation_partLists", x => x.id);
                    table.ForeignKey(
                        name: "FK_quotation_partLists_parts_PartId",
                        column: x => x.PartId,
                        principalSchema: "AOGsystem",
                        principalTable: "parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quotation_partLists_quotations_QuotationId",
                        column: x => x.QuotationId,
                        principalSchema: "AOGsystem",
                        principalTable: "quotations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_companies_CompanyId",
                schema: "AOGsystem",
                table: "companies",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_home_base_follow_ups_PartId",
                schema: "AOGsystem",
                table: "home_base_follow_ups",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_parts_HomeBaseFollowUpId",
                schema: "AOGsystem",
                table: "parts",
                column: "HomeBaseFollowUpId");

            migrationBuilder.CreateIndex(
                name: "IX_parts_QuotationPartListId",
                schema: "AOGsystem",
                table: "parts",
                column: "QuotationPartListId");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_partLists_PartId",
                schema: "AOGsystem",
                table: "quotation_partLists",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_quotation_partLists_QuotationId",
                schema: "AOGsystem",
                table: "quotation_partLists",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_remarks_HomeBaseFollowUpId",
                schema: "AOGsystem",
                table: "remarks",
                column: "HomeBaseFollowUpId");

            migrationBuilder.CreateIndex(
                name: "IX_remarks_RemarkId",
                schema: "AOGsystem",
                table: "remarks",
                column: "RemarkId");

            migrationBuilder.AddForeignKey(
                name: "part_id",
                schema: "AOGsystem",
                table: "home_base_follow_ups",
                column: "PartId",
                principalSchema: "AOGsystem",
                principalTable: "parts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_parts_quotation_partLists_QuotationPartListId",
                schema: "AOGsystem",
                table: "parts",
                column: "QuotationPartListId",
                principalSchema: "AOGsystem",
                principalTable: "quotation_partLists",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quotation_partLists_quotations_QuotationId",
                schema: "AOGsystem",
                table: "quotation_partLists");

            migrationBuilder.DropForeignKey(
                name: "part_id",
                schema: "AOGsystem",
                table: "home_base_follow_ups");

            migrationBuilder.DropForeignKey(
                name: "FK_quotation_partLists_parts_PartId",
                schema: "AOGsystem",
                table: "quotation_partLists");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "remarks",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "quotations",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "parts",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "home_base_follow_ups",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "quotation_partLists",
                schema: "AOGsystem");
        }
    }
}
