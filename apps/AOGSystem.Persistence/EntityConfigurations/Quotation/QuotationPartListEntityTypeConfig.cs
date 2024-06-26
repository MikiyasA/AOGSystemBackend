﻿using AOGSystem.Domain.Quotation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.Quotation
{
    public class QuotationPartListEntityTypeConfig : IEntityTypeConfiguration<QuotationPartList>
    {
        public void Configure(EntityTypeBuilder<QuotationPartList> builder)
        {
            builder.ToTable("quotation_partLists", AOGSystemContext.DefaultSchema);
            builder.HasKey(x => x.Id);

            builder.Property(q => q.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(q => q.CreatedAT)
                .HasColumnName("created_at");

            builder.Property(q => q.UpdatedAT)
                .HasColumnName("updated_at");

            builder.Property(q => q.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(q => q.UpdatedBy)
                .HasColumnName("updated_by");

            builder.HasOne(q => q.Part)
                .WithMany()
                .HasForeignKey(q => q.PartId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(); 

            builder.Property(q => q.CurrentPrice)
                 .HasColumnName("current_price")
                 .IsRequired();

            builder.Property(q => q.SalesPrice)
                .HasColumnName("sales_price");

            builder.Property(q => q.FixedLoanPrice)
                .HasColumnName("fixed_loan_price");

            builder.Property(q => q.LoanPricePerDay)
                .HasColumnName("loan_price_per_day");

            builder.Property(q => q.ExchangePrice)
                .HasColumnName("exchange_price");

            builder.Property(q => q.StockLocation)
                .HasColumnName("stock_location");

            builder.Property(q => q.Condition)
                .HasColumnName("condition");

            builder.HasOne<Domain.Quotation.Quotation>()
                .WithMany(q => q.QuotationPartsLists)
                .HasForeignKey(q => q.QuotationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
