using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.FollowUp
{
    internal class AOGFollowUpEntityTypeConfig : IEntityTypeConfiguration<AOGFollowUp>
    {
        public void Configure(EntityTypeBuilder<AOGFollowUp> builder)
        {
            builder.ToTable("aog_follow_ups", AOGSystemContext.DefaultSchema);
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

            builder.Property(x => x.RID)
                .HasColumnName("rid")
                .IsRequired();

            builder.Property(x => x.RequestDate)
                .HasColumnName("request_date")
                .IsRequired();

            builder.Property(x => x.AirCraft)
                .HasColumnName("air_craft")
                .IsRequired();

            builder.Property(x => x.WorkLocation)
                .HasColumnName("work_location");

            builder.Property(x => x.AOGStation)
                .HasColumnName("aog_station");

            builder.Property(x => x.TailNo)
                .HasColumnName("tail_no")
                .IsRequired();

            builder.Property(x => x.Customer)
                .HasColumnName("customer")
                .IsRequired();
            builder.Property(x => x.PartId)
                .HasColumnName("part_id")
                .IsRequired();

            builder.Property(x => x.PONumber)
                .HasColumnName("po_number");

            builder.Property(x => x.OrderType)
                .HasColumnName("order_type");

            builder.Property(x => x.Quantity)
                .HasColumnName("quantity");

            builder.Property(x => x.UOM)
                .HasColumnName("uom");

            builder.Property(x => x.Vendor)
                .HasColumnName("vendor");

            builder.Property(x => x.EDD)
                .HasColumnName("edd");

            builder.Property(x => x.Status)
                .HasColumnName("status");

            builder.Property(x => x.AWBNo)
                .HasColumnName("awb_no");

            builder.Property(x => x.FlightNo)
                .HasColumnName("flight_no");

            builder.Property(x => x.NeedHigherMgntAttn)
                .HasColumnName("need_higher_mgnt_attn");

            builder.HasOne<FollowUpTabs>()
                .WithMany(x => x.FollowUps)
                .HasForeignKey(x => x.FollowUpTabsId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.FollowUpTabsId)
                .HasColumnName("follow_up_tabs");
        }
    }
}
