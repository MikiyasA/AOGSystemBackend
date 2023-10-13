﻿// <auto-generated />
using System;
using AOGSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AOGSystem.Persistence.Migrations
{
    [DbContext(typeof(AOGSystemContext))]
    [Migration("20230924062031_addFixedAndLoanPerDatPrice")]
    partial class addFixedAndLoanPerDatPrice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AOGSystem.Domain.FollowUp.AOGFollowUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("AOGStation")
                        .HasColumnType("longtext")
                        .HasColumnName("aog_station");

                    b.Property<string>("AWBNo")
                        .HasColumnType("longtext")
                        .HasColumnName("awb_no");

                    b.Property<string>("AirCraft")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("air_craft");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("created_by");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("customer");

                    b.Property<DateTime?>("EDD")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("edd");

                    b.Property<bool>("NeedHigherMgntAttn")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("need_higher_mgnt_attn");

                    b.Property<string>("OrderType")
                        .HasColumnType("longtext")
                        .HasColumnName("order_type");

                    b.Property<string>("PONumber")
                        .HasColumnType("longtext")
                        .HasColumnName("po_number");

                    b.Property<int?>("PartId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("part_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("RID")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("rid");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("request_date");

                    b.Property<string>("Status")
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.Property<string>("TailNo")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("tail_no");

                    b.Property<string>("UOM")
                        .HasColumnType("longtext")
                        .HasColumnName("uom");

                    b.Property<DateTime?>("UpdatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("updated_by");

                    b.Property<string>("Vendor")
                        .HasColumnType("longtext")
                        .HasColumnName("vendor");

                    b.Property<string>("WorkLocation")
                        .HasColumnType("longtext")
                        .HasColumnName("work_location");

                    b.HasKey("Id");

                    b.HasIndex("PartId");

                    b.ToTable("aog_follow_ups", "AOGsystem");
                });

            modelBuilder.Entity("AOGSystem.Domain.FollowUp.Remark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("AOGFollowUpId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("created_by");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("message");

                    b.Property<DateTime?>("UpdatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("AOGFollowUpId");

                    b.ToTable("remarks", "AOGsystem");
                });

            modelBuilder.Entity("AOGSystem.Domain.General.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("address");

                    b.Property<string>("BillToAddress")
                        .HasColumnType("longtext")
                        .HasColumnName("bill_to_address");

                    b.Property<string>("City")
                        .HasColumnType("longtext")
                        .HasColumnName("city");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("code");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("country");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("created_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<string>("PaymentTerm")
                        .HasColumnType("longtext")
                        .HasColumnName("payment_term");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext")
                        .HasColumnName("phone");

                    b.Property<string>("ShipToAddress")
                        .HasColumnType("longtext")
                        .HasColumnName("ship_to_address");

                    b.Property<DateTime?>("UpdatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.ToTable("companies", "AOGsystem");
                });

            modelBuilder.Entity("AOGSystem.Domain.General.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<string>("FinancialClass")
                        .HasColumnType("longtext")
                        .HasColumnName("financial_class");

                    b.Property<string>("PartNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("part_number");

                    b.Property<int?>("QuotationPartListId")
                        .HasColumnType("int");

                    b.Property<string>("StockNo")
                        .HasColumnType("longtext")
                        .HasColumnName("stock_no");

                    b.Property<DateTime?>("UpdatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("QuotationPartListId");

                    b.ToTable("parts", "AOGsystem");
                });

            modelBuilder.Entity("AOGSystem.Domain.General.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdatedAT")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AOGSystem.Domain.Quotation.Quotation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("company_id");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("created_by");

                    b.Property<bool>("Exchange")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("exchange");

                    b.Property<bool>("Loan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("loan");

                    b.Property<int?>("OfferedById")
                        .HasColumnType("int")
                        .HasColumnName("offered_by_id");

                    b.Property<string>("RequestedByEmail")
                        .HasColumnType("longtext")
                        .HasColumnName("requested_by_email");

                    b.Property<string>("RequestedByName")
                        .HasColumnType("longtext")
                        .HasColumnName("requested_by_name");

                    b.Property<string>("RequestedByPhone")
                        .HasColumnType("longtext")
                        .HasColumnName("requested_by_phone");

                    b.Property<bool>("Sales")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false)
                        .HasColumnName("sales");

                    b.Property<DateTime?>("UpdatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("OfferedById");

                    b.ToTable("quotations", "AOGsystem");
                });

            modelBuilder.Entity("AOGSystem.Domain.Quotation.QuotationPartList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Condition")
                        .HasColumnType("longtext")
                        .HasColumnName("condition");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("created_by");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("current_price");

                    b.Property<decimal>("ExchangePrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("exchange_price");

                    b.Property<decimal>("FixedLoanPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("fixed_loan_price");

                    b.Property<decimal>("LoanPricePerDay")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("loan_price_per_day");

                    b.Property<int>("PartId")
                        .HasColumnType("int");

                    b.Property<int>("QuotationId")
                        .HasColumnType("int");

                    b.Property<decimal>("SalesPrice")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("sales_price");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("StockLocation")
                        .HasColumnType("longtext")
                        .HasColumnName("stock_location");

                    b.Property<DateTime?>("UpdatedAT")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("longtext")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("PartId");

                    b.HasIndex("QuotationId");

                    b.ToTable("quotation_partLists", "AOGsystem");
                });

            modelBuilder.Entity("AOGSystem.Domain.FollowUp.AOGFollowUp", b =>
                {
                    b.HasOne("AOGSystem.Domain.General.Part", "Part")
                        .WithMany()
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Part");
                });

            modelBuilder.Entity("AOGSystem.Domain.FollowUp.Remark", b =>
                {
                    b.HasOne("AOGSystem.Domain.FollowUp.AOGFollowUp", null)
                        .WithMany("Remarks")
                        .HasForeignKey("AOGFollowUpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AOGSystem.Domain.General.Part", b =>
                {
                    b.HasOne("AOGSystem.Domain.Quotation.QuotationPartList", null)
                        .WithMany("Parts")
                        .HasForeignKey("QuotationPartListId");
                });

            modelBuilder.Entity("AOGSystem.Domain.Quotation.Quotation", b =>
                {
                    b.HasOne("AOGSystem.Domain.General.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AOGSystem.Domain.General.User", "OfferedBy")
                        .WithMany()
                        .HasForeignKey("OfferedById");

                    b.Navigation("Company");

                    b.Navigation("OfferedBy");
                });

            modelBuilder.Entity("AOGSystem.Domain.Quotation.QuotationPartList", b =>
                {
                    b.HasOne("AOGSystem.Domain.General.Part", "Part")
                        .WithMany()
                        .HasForeignKey("PartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AOGSystem.Domain.Quotation.Quotation", null)
                        .WithMany("QuotationPartsLists")
                        .HasForeignKey("QuotationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Part");
                });

            modelBuilder.Entity("AOGSystem.Domain.FollowUp.AOGFollowUp", b =>
                {
                    b.Navigation("Remarks");
                });

            modelBuilder.Entity("AOGSystem.Domain.Quotation.Quotation", b =>
                {
                    b.Navigation("QuotationPartsLists");
                });

            modelBuilder.Entity("AOGSystem.Domain.Quotation.QuotationPartList", b =>
                {
                    b.Navigation("Parts");
                });
#pragma warning restore 612, 618
        }
    }
}
