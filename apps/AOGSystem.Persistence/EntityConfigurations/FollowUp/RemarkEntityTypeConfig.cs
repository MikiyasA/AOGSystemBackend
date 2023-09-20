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
    public class RemarkEntityTypeConfig : IEntityTypeConfiguration<Remark>
    {
        public void Configure(EntityTypeBuilder<Remark> builder)
        {
            builder.ToTable("remarks", AOGSystemContext.DefaultSchema);
            builder.HasKey(x => x.Id);

            builder.Property(q => q.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.CreatedAT)
                .HasColumnName("created_at");

            builder.Property(q => q.UpdatedAT)
                .HasColumnName("updated_at");

            builder.Property(q => q.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(q => q.UpdatedBy)
                .HasColumnName("updated_by");

            builder.Property(x => x.Message)
                .HasColumnName("message")
                .IsRequired();

            builder.HasOne<AOGFollowUp>()
                .WithMany(x => x.Remarks)
                .HasForeignKey(x => x.AOGFollowUpId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
