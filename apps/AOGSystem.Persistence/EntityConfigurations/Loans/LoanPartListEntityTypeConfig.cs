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
    public class LoanPartListEntityTypeConfig : IEntityTypeConfiguration<LoanPartList>
    {
        public void Configure(EntityTypeBuilder<LoanPartList> builder)
        {
            builder.ToTable("loan_part_lists", AOGSystemContext.DefaultSchema);
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

            builder.Property(x => x.PartId)
                .HasColumnName("part_id")
                .IsRequired();

            builder.HasOne<Part>()
                .WithMany()
                .HasForeignKey(x => x.PartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Quantity)
                .HasColumnName("quantity")
                .IsRequired();
            builder.Property(x => x.UOM)
                .HasColumnName("uom")
                .IsRequired();
            builder.Property(x => x.SerialNo)
                .HasColumnName("serila_no")
                .IsRequired(false);
            builder.Property(x => x.RID)
                .HasColumnName("rid")
                .IsRequired(false);
            builder.Property(x => x.ShipDate)
                .HasColumnName("ship_date")
                .IsRequired(false);
            builder.Property(x => x.ShippingReference)
                .HasColumnName("shipping_reference")
                .IsRequired(false);
            builder.Property(x => x.ReceivedDate)
                .HasColumnName("received_date")
                .IsRequired(false);
            builder.Property(x => x.ReceivingReference)
                .HasColumnName("receiving_reference")
                .IsRequired(false);
            builder.Property(x => x.ReceivingDefect)
                .HasColumnName("receiving_defect")
                .IsRequired(false);
            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();
            builder.Property(x => x.IsInvoiced)
                .HasColumnName("is_invoice");
        }
    }
}
