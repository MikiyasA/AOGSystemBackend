using AOGSystem.Domain.SOA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.SOA
{
    public class InvoiceListEntityTypeConfig : IEntityTypeConfiguration<InvoiceList>
    {
        public void Configure(EntityTypeBuilder<InvoiceList> builder)
        {
            builder.ToTable("invoice_list", AOGSystemContext.DefaultSchema);
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
            builder.Property(x => x.PONo)
                .HasColumnName("po_no")
                .IsRequired();
            builder.Property(x => x.InvoiceDate)
                .HasColumnName("invoice_date")
                .IsRequired();
            builder.Property(x => x.DueDate)
                .HasColumnName("due_date")
                .IsRequired();
            builder.Property(x => x.Amount)
                .HasColumnName("amount")
                .IsRequired();
            builder.Property(x => x.Currency)
                .HasColumnName("currency")
                .IsRequired();
            builder.Property(x => x.UnderForllowup)
                .HasColumnName("under_forllowup")
                .IsRequired(false);
            builder.Property(x => x.PaymentProcessedDate)
                .HasColumnName("payment_processed_date")
                .IsRequired(false);
            builder.Property(x => x.POPDate)
                .HasColumnName("pod_date")
                .IsRequired(false);
            builder.Property(x => x.POPReference)
                .HasColumnName("pop_reference")
                .IsRequired(false);
            builder.Property(x => x.ChargeType)
                .HasColumnName("charge_type")
                .IsRequired(false);
            builder.Property(x => x.BuyerName)
                .HasColumnName("buyer_name")
                .IsRequired(false);
            builder.Property(x => x.TLName)
                .HasColumnName("TL_name")
                .IsRequired(false);
            builder.Property(x => x.ManagerName)
                .HasColumnName("manager_name")
                .IsRequired(false);
            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired(false);
        }
    }
}
