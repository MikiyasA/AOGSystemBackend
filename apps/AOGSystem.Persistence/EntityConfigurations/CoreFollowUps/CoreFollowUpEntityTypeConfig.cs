using AOGSystem.Domain.CoreFollowUps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.CoreFollowUps
{
    public class CoreFollowUpEntityTypeConfig : IEntityTypeConfiguration<CoreFollowUp>
    {
        public void Configure(EntityTypeBuilder<CoreFollowUp> builder)
        {
            builder.ToTable("core_follow_ups", AOGSystemContext.DefaultSchema);
            builder.HasKey(x => x.Id);

            builder.Property(q => q.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.CreatedAT)
                .HasColumnName("created_at");

            builder.Property(q => q.UpdatedAT)
                .HasColumnName("updated_at")
                .IsRequired(false);

            builder.Property(q => q.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(q => q.UpdatedBy)
                .HasColumnName("updated_by");

            builder.Property(x => x.PONo)
                .HasColumnName("po_no")
                .IsRequired();

            builder.Property(x => x.POCreatedDate)
                .HasColumnName("po_created_date");

            builder.Property(x => x.Aircraft)
                .HasColumnName("aircraft");

            builder.Property(x => x.TailNo)
                .HasColumnName("tail_no");

            builder.Property(x => x.PartNumber)
                .HasColumnName("part_number");

            builder.Property(x => x.Description)
                .HasColumnName("description");

            builder.Property(x => x.StockNo)
                .HasColumnName("stock_no");

            builder.Property(x => x.Vendor)
                .HasColumnName("vendor");

            builder.Property(x => x.PartReceiveDate)
                .HasColumnName("part_receive_date");

            builder.Property(x => x.ReturnDueDate)
                .HasColumnName("return_due_date");

            builder.Property(x => x.ReturnProcessedDate)
                .HasColumnName("return_processed_date");

            builder.Property(x => x.AWBNo)
                .HasColumnName("awb_no");

            builder.Property(x => x.ReturnedPart)
                .HasColumnName("returned_part");

            builder.Property(x => x.PODDate)
                .HasColumnName("pod_date");

            builder.Property(x => x.Remark)
                .HasColumnName("remark");

            builder.Property(x => x.Status)
                .HasColumnName("status");
        }
    }
}
