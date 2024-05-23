using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class intialSqlMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AOGsystem");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "assignment",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    start_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    expected_finished_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    finished_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    finished_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    assigned_to = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reassigned_to = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reassigned_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reassigned_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reopned_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reopned_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    sype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ship_to_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bill_to_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "core_follow_ups",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    po_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    po_created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    aircraft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tail_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    part_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stock_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    part_released_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    part_receive_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    return_due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    return_processed_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    awb_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    returned_part = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pod_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_follow_ups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cost_saving",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    old_po = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    new_po = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    issue_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cn_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    old_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    new_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    price_variance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    saving_in_USD = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    saving_in_ETB = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_purchase_order = table.Column<bool>(type: "bit", nullable: true),
                    is_repair_order = table.Column<bool>(type: "bit", nullable: true),
                    saved_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cost_saving", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "follow_tabs",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_follow_tabs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vendor",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    vendor_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vendor_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vendor_account_manager_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vendor_account_manager_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vendor_finance_contact_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vendor_finance_contact_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    credit_limit = table.Column<double>(type: "float", nullable: true),
                    total_outstanding = table.Column<double>(type: "float", nullable: true),
                    under_process = table.Column<double>(type: "float", nullable: true),
                    under_dispute = table.Column<double>(type: "float", nullable: true),
                    paid_amount = table.Column<double>(type: "float", nullable: true),
                    et_finance_contact_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    et_finance_contact_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    soa_handler_buyer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    soa_handler_buyer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    certificate_expiry_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attachment_links",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    attachment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entity_type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachment_links", x => new { x.id, x.attachment_id, x.entity_id, x.entity_type });
                    table.ForeignKey(
                        name: "FK_attachment_links_attachments_attachment_id",
                        column: x => x.attachment_id,
                        principalSchema: "AOGsystem",
                        principalTable: "attachments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loans",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_order_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ordered_by_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ordered_by_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ship_to_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_approve = table.Column<bool>(type: "bit", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loans", x => x.id);
                    table.ForeignKey(
                        name: "FK_loans_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "AOGsystem",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "quotations",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    loan = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    sales = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    exchange = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    requested_by_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requested_by_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    requested_by_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    offered_by_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotations", x => x.id);
                    table.ForeignKey(
                        name: "FK_quotations_AspNetUsers_offered_by_id",
                        column: x => x.offered_by_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quotations_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "AOGsystem",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    company_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ordered_by_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_by_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_order_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ship_to_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_approve = table.Column<bool>(type: "bit", nullable: false),
                    is_fully_shipped = table.Column<bool>(type: "bit", nullable: false),
                    awb_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ship_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    received_bu_customer = table.Column<bool>(type: "bit", nullable: false),
                    received_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.id);
                    table.ForeignKey(
                        name: "FK_sales_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "AOGsystem",
                        principalTable: "companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PONo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnderFollowup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POPDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    POPReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TLName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLists_vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "AOGsystem",
                        principalTable: "vendor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    invoice_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    invoice_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sales_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    loan_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    pop_reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pop_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice", x => x.id);
                    table.ForeignKey(
                        name: "FK_invoice_sales_sales_order_id",
                        column: x => x.sales_order_id,
                        principalSchema: "AOGsystem",
                        principalTable: "sales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "buyer_remark",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyer_remark", x => x.id);
                    table.ForeignKey(
                        name: "FK_buyer_remark_InvoiceLists_InvoiceListId",
                        column: x => x.InvoiceListId,
                        principalTable: "InvoiceLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "finance_remark",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_finance_remark", x => x.id);
                    table.ForeignKey(
                        name: "FK_finance_remark_InvoiceLists_InvoiceListId",
                        column: x => x.InvoiceListId,
                        principalTable: "InvoiceLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "aog_follow_ups",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    request_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    air_craft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tail_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    work_location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    aog_station = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    po_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    uom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    edd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    awb_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    flight_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    need_higher_mgnt_attn = table.Column<bool>(type: "bit", nullable: false),
                    part_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    follow_up_tabs = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aog_follow_ups", x => x.id);
                    table.ForeignKey(
                        name: "FK_aog_follow_ups_follow_tabs_follow_up_tabs",
                        column: x => x.follow_up_tabs,
                        principalSchema: "AOGsystem",
                        principalTable: "follow_tabs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "remarks",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AOGFollowUpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_remarks", x => x.id);
                    table.ForeignKey(
                        name: "FK_remarks_aog_follow_ups_AOGFollowUpId",
                        column: x => x.AOGFollowUpId,
                        principalSchema: "AOGsystem",
                        principalTable: "aog_follow_ups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_part_list",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    part_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    uom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unit_price = table.Column<double>(type: "float", nullable: false),
                    total_price = table.Column<double>(type: "float", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serila_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    offers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoice_part_list", x => x.id);
                    table.ForeignKey(
                        name: "FK_invoice_part_list_invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "AOGsystem",
                        principalTable: "invoice",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "loan_part_lists",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    part_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    uom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    serila_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ship_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    shipping_reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    received_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    receiving_reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    receiving_defect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    is_invoice = table.Column<bool>(type: "bit", nullable: false),
                    LoanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loan_part_lists", x => x.id);
                    table.ForeignKey(
                        name: "FK_loan_part_lists_loans_LoanId",
                        column: x => x.LoanId,
                        principalSchema: "AOGsystem",
                        principalTable: "loans",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "offers",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    base_price = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<double>(type: "float", nullable: false),
                    total_price = table.Column<double>(type: "float", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoanPartListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offers", x => x.id);
                    table.ForeignKey(
                        name: "FK_offers_loan_part_lists_LoanPartListId",
                        column: x => x.LoanPartListId,
                        principalSchema: "AOGsystem",
                        principalTable: "loan_part_lists",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "parts",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    part_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    financial_class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    part_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuotationPartListId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quotation_partLists",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuotationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    current_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sales_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fixed_loan_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    loan_price_per_day = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    exchange_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock_location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "sales_part_list",
                schema: "AOGsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    part_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    uom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unit_price = table.Column<double>(type: "float", nullable: false),
                    total_price = table.Column<double>(type: "float", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serila_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    is_invoice = table.Column<bool>(type: "bit", nullable: false),
                    SalesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    updated_by = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_part_list", x => x.id);
                    table.ForeignKey(
                        name: "FK_sales_part_list_parts_part_id",
                        column: x => x.part_id,
                        principalSchema: "AOGsystem",
                        principalTable: "parts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sales_part_list_sales_SalesId",
                        column: x => x.SalesId,
                        principalSchema: "AOGsystem",
                        principalTable: "sales",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_aog_follow_ups_follow_up_tabs",
                schema: "AOGsystem",
                table: "aog_follow_ups",
                column: "follow_up_tabs");

            migrationBuilder.CreateIndex(
                name: "IX_aog_follow_ups_part_id",
                schema: "AOGsystem",
                table: "aog_follow_ups",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_attachment_links_attachment_id",
                schema: "AOGsystem",
                table: "attachment_links",
                column: "attachment_id");

            migrationBuilder.CreateIndex(
                name: "IX_buyer_remark_InvoiceListId",
                schema: "AOGsystem",
                table: "buyer_remark",
                column: "InvoiceListId");

            migrationBuilder.CreateIndex(
                name: "IX_finance_remark_InvoiceListId",
                schema: "AOGsystem",
                table: "finance_remark",
                column: "InvoiceListId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_sales_order_id",
                schema: "AOGsystem",
                table: "invoice",
                column: "sales_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_part_list_InvoiceId",
                schema: "AOGsystem",
                table: "invoice_part_list",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_part_list_part_id",
                schema: "AOGsystem",
                table: "invoice_part_list",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLists_VendorId",
                table: "InvoiceLists",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_loan_part_lists_LoanId",
                schema: "AOGsystem",
                table: "loan_part_lists",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_loan_part_lists_part_id",
                schema: "AOGsystem",
                table: "loan_part_lists",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "IX_loans_company_id",
                schema: "AOGsystem",
                table: "loans",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_offers_LoanPartListId",
                schema: "AOGsystem",
                table: "offers",
                column: "LoanPartListId");

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
                name: "IX_quotations_company_id",
                schema: "AOGsystem",
                table: "quotations",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_quotations_offered_by_id",
                schema: "AOGsystem",
                table: "quotations",
                column: "offered_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_remarks_AOGFollowUpId",
                schema: "AOGsystem",
                table: "remarks",
                column: "AOGFollowUpId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_company_id",
                schema: "AOGsystem",
                table: "sales",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_part_list_part_id",
                schema: "AOGsystem",
                table: "sales_part_list",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "IX_sales_part_list_SalesId",
                schema: "AOGsystem",
                table: "sales_part_list",
                column: "SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_aog_follow_ups_parts_part_id",
                schema: "AOGsystem",
                table: "aog_follow_ups",
                column: "part_id",
                principalSchema: "AOGsystem",
                principalTable: "parts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_part_list_parts_part_id",
                schema: "AOGsystem",
                table: "invoice_part_list",
                column: "part_id",
                principalSchema: "AOGsystem",
                principalTable: "parts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_loan_part_lists_parts_part_id",
                schema: "AOGsystem",
                table: "loan_part_lists",
                column: "part_id",
                principalSchema: "AOGsystem",
                principalTable: "parts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_quotation_partLists_parts_PartId",
                schema: "AOGsystem",
                table: "quotation_partLists");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "assignment",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "attachment_links",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "buyer_remark",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "core_follow_ups",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "cost_saving",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "finance_remark",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "invoice_part_list",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "offers",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "remarks",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "sales_part_list",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "attachments",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "InvoiceLists");

            migrationBuilder.DropTable(
                name: "invoice",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "loan_part_lists",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "aog_follow_ups",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "vendor",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "sales",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "loans",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "follow_tabs",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "parts",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "quotation_partLists",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "quotations",
                schema: "AOGsystem");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "AOGsystem");
        }
    }
}
