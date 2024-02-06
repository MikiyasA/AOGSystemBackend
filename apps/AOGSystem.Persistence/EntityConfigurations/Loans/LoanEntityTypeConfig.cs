using AOGSystem.Domain.General;
using AOGSystem.Domain.Loans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.Loans
{
    public class LoanEntityTypeConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("loans", AOGSystemContext.DefaultSchema);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAT)
                .HasColumnName("created_at");

            builder.Property(x => x.UpdatedAT)
                .HasColumnName("updated_at")
                .IsRequired(false);

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by");

            builder.Property(x => x.OrderNo)
                .HasJsonPropertyName("order_no");

            builder.Property(x => x.CompanyId)
                .HasColumnName("company_id")
                .IsRequired();

            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CustomerOrderNo)
                .HasColumnName("customer_order_no")
                .IsRequired(false);

            builder.Property(x => x.OrderedByName)
                .HasColumnName("ordered_by_name")
                .IsRequired(false);

            builder.Property(x => x.OrderedByEmail)
                .HasColumnName("ordered_by_email")
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasJsonPropertyName("status")
                .IsRequired();

            builder.Property(x => x.IsApproved)
                .HasColumnName("is_approve");
            builder.Property(x => x.Note)
                .HasColumnName("note")
                .IsRequired(false);

        }
    }
}
