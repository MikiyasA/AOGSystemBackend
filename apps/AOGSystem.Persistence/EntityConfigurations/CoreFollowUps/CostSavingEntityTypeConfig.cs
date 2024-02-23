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
    public class CostSavingEntityTypeConfig : IEntityTypeConfiguration<CostSaving>
    {
        public void Configure(EntityTypeBuilder<CostSaving> builder)
        {
            builder.ToTable("cost_saving", AOGSystemContext.DefaultSchema);
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

            builder.Property(q => q.OldPO)
                .HasColumnName("old_po");
            builder.Property(q => q.NewPO)
                .HasColumnName("new_po");
            builder.Property(q => q.IssueDate)
                .HasColumnName("issue_date");
            builder.Property(q => q.CNDate)
                .HasColumnName("cn_date");
            builder.Property(q => q.OldPrice)
                .HasColumnName("old_price");
            builder.Property(q => q.NewPrice)
                .HasColumnName("new_price");
            builder.Property(q => q.PriceVariance)
                .HasColumnName("price_variance");
            builder.Property(q => q.Quantity)
                .HasColumnName("quantity");
            builder.Property(q => q.SavingInUSD)
                .HasColumnName("saving_in_USD");
            builder.Property(q => q.SavingInETB)
                .HasColumnName("saving_in_ETB");
            builder.Property(q => q.Remark)
                .HasColumnName("remark");
            builder.Property(q => q.IsPurchaseOrder)
                .HasColumnName("is_purchase_order");
            builder.Property(q => q.IsRepairOrder)
                .HasColumnName("is_repair_order");
            builder.Property(q => q.SavedBy)
                .HasColumnName("saved_by");
            builder.Property(q => q.Status)
                .HasColumnName("status");
        }
    }
}
