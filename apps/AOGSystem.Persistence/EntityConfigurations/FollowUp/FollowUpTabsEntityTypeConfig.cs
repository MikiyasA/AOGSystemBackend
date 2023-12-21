using AOGSystem.Domain.FollowUp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.FollowUp
{
    public class FollowUpTabsEntityTypeConfig : IEntityTypeConfiguration<FollowUpTabs>
    {
        public void Configure(EntityTypeBuilder<FollowUpTabs> builder)
        {
            builder.ToTable("follow_tabs", AOGSystemContext.DefaultSchema);
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

            builder.Property(q => q.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(q => q.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(x => x.Color)
                .HasColumnName("color")
                .IsRequired();
        }
    }
}
