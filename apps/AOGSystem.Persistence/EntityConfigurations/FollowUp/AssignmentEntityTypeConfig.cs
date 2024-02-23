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
    public class AssignmentEntityTypeConfig : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("assignment", AOGSystemContext.DefaultSchema);
            builder.HasKey(a => a.Id);

            builder.Property(x => x.Id)
               .HasColumnName("id")
               .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAT)
                .HasColumnName("created_at");

            builder.Property(x => x.UpdatedAT)
                .HasColumnName("updated_at");

            builder.Property(x => x.CreatedBy)
                .HasColumnName("created_by");

            builder.Property(x => x.UpdatedBy)
                .HasColumnName("updated_by");

            builder.Property(x => x.Title)
                .HasColumnName("title")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired(false);

            builder.Property(x => x.StartDate)
                .HasColumnName("start_date")
                .IsRequired(false);

            builder.Property(x => x.StartBy)
                .HasColumnName("start_by")
                .IsRequired(false);

            builder.Property(x => x.DueDate)
                .HasColumnName("due_date")
                .IsRequired(false);

            builder.Property(x => x.ExpectedFinishedDate)
                .HasColumnName("expected_finished_date")
                .IsRequired(false);

            builder.Property(x => x.FinishedDate)
                .HasColumnName("finished_date")
                .IsRequired(false);

            builder.Property(x => x.FinishedBy)
                .HasColumnName("finished_by")
                .IsRequired(false);

            builder.Property(x => x.AssignedTo)
                .HasColumnName("assigned_to")
                .IsRequired(false);

            builder.Property(x => x.ReAssignedTo)
                .HasColumnName("reassigned_to")
                .IsRequired(false);

            builder.Property(x => x.ReAssignedBy) 
                .HasColumnName("reassigned_by")
                .IsRequired(false);

            builder.Property(x => x.ReAssignedAt) 
                .HasColumnName("reassigned_at")
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired();

            builder.Property(x => x.ReOpenedBy)
                .HasColumnName("reopned_by")
                .IsRequired(false);

            builder.Property(x => x.ReOpenedAt)
                .HasColumnName("reopned_at")
                .IsRequired(false);
        }
    }
}
