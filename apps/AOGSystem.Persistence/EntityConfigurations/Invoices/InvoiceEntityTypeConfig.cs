using AOGSystem.Domain.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.Invoices
{
    public class InvoiceEntityTypeConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoice", AOGSystemContext.DefaultSchema);
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

            builder.Property(x => x.InvoiceNo)
                .HasColumnName("invoice_no")
                .IsRequired();
            builder.Property(x => x.InvoiceDate)
                .HasColumnName("invoice_date")
                .IsRequired();
            builder.Property(x => x.DueDate)
                .HasColumnName("due_date")
                .IsRequired();
            builder.Property(x => x.SalesOrderId)
                .HasColumnName("sales_order_id")
                .IsRequired(false);
            builder.HasOne<Domain.Sales.Sales>()
                .WithMany()
                .HasForeignKey(x => x.SalesOrderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.LoanOrderId)
                .HasColumnName("loan_order_id")
                .IsRequired(false);
            //builder.HasOne<Loan>()
            //    .WithMany()
            //    .HasForeignKey(x => x.LoanOrderId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.TransactionType)
                .HasColumnName("transaction_type")
                .IsRequired();
            builder.Property(x => x.IsApproved)
                .HasColumnName("is_approved")
                .IsRequired();
            builder.Property(x => x.POPReference)
                .HasColumnName("pop_reference")
                .IsRequired(false);
            builder.Property(x => x.POPDate)
                .HasColumnName("pop_date")
                .IsRequired(false);
            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired(false);
            builder.Property(x => x.Remark)
                .HasColumnName("remark")
                .IsRequired(false);

        }
    }
}
