﻿using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations
{
    public class QuotationEntityTypeConfig : IEntityTypeConfiguration<Domain.Quotation.Quotation>
    {
        public void Configure(EntityTypeBuilder<Domain.Quotation.Quotation> builder)
        {
            builder.ToTable("quotations", AOGSystemContext.DefaultSchema);
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.CreatedAT)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.UpdatedAT)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP") 
                .ValueGeneratedOnUpdate();

            builder.Property(q => q.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(q => q.UpdatedBy)
                .HasColumnName("created_by");

            builder.Property(q => q.Loan)
                .HasColumnName("loan")
                .HasDefaultValue(false);

            builder.Property(q => q.Sales)
                .HasColumnName("loan")
                .HasDefaultValue(false);

            builder.Property(q => q.Exchange)
                .HasColumnName("loan")
                .HasDefaultValue(false);

            builder.HasOne(q => q.Company) // Quotation has one Company.
            .WithMany(c => c.Quotations) // Company has many Quotations.
            .HasForeignKey(q => q.CompanyId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            builder.Property(q => q.RequestedByName)
                .HasColumnName("requested_by_name");

            builder.Property(q => q.RequestedByEmail)
                .HasColumnName("requested_by_email");

            builder.Property(q => q.RequestedByPhone)
                .HasColumnName("requested_by_phone");

        }
    }
}