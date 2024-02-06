using AOGSystem.Domain.General;
using AOGSystem.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.Sales
{
    public class SalesPartListEntityTypeConfig : IEntityTypeConfiguration<SalesPartList>
    {
        public void Configure(EntityTypeBuilder<SalesPartList> builder)
        {
            builder.ToTable("sales_part_list", AOGSystemContext.DefaultSchema);
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
            builder.Property(x => x.UnitPrice)
                .HasColumnName("unit_price")
                .IsRequired();
            builder.Property(x => x.TotalPrice)
                .HasColumnName("total_price")
                .IsRequired();
            builder.Property(x => x.Currency)
                .HasColumnName("currency")
                .IsRequired();
            builder.Property(x => x.RID)
                .HasColumnName("rid")
                .IsRequired(false);
            builder.Property(x => x.SerialNo)
                .HasColumnName("serila_no")
                .IsRequired(false);
            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted")
                .IsRequired();
            builder.Property(x => x.IsInvoiced)
                .HasColumnName("is_invoice");


        }
    }
}
