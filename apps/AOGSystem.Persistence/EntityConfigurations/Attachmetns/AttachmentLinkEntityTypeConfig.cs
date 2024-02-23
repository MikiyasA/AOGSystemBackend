using AOGSystem.Domain.Attachments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.Attachmetns
{
    public class AttachmentLinkEntityTypeConfig : IEntityTypeConfiguration<AttachmentLink>
    {
        public void Configure(EntityTypeBuilder<AttachmentLink> builder)
        {
            builder.ToTable("attachment_links", AOGSystemContext.DefaultSchema);
            builder.HasKey(x => new { x.Id, x.AttachmentId, x.EntityId, x.EntityType });

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

            builder.Property(x => x.AttachmentId)
                .HasColumnName("attachment_id");
            //builder.Property(x => x.Attachment)
            //    .HasColumnName("attachment");
            builder.Property(x => x.EntityId)
                .HasColumnName("entity_id");
            builder.Property(x => x.EntityType)
                .HasColumnName("entity_type");
        }
    }
}
