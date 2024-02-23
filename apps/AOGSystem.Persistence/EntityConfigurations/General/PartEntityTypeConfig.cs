using AOGSystem.Domain.Attachments;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.General
{
    public class PartEntityTypeConfig : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.ToTable("parts", AOGSystemContext.DefaultSchema);
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

            builder.Property(x => x.PartNumber)
                .HasColumnName("part_number")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired();

            builder.Property(x => x.StockNo)
                .HasColumnName("stock_no");

            builder.Property(x => x.FinancialClass)
                .HasColumnName("financial_class");


            builder.HasMany<AOGFollowUp>()
                .WithOne(x => x.Part)
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasMany<QuotationPartList>()
                .WithOne(x => x.Part)
                .HasForeignKey(x => x.PartId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(x => x.Manufacturer)
                .HasColumnName("manufacturer");

            builder.Property(x => x.PartType)
                .HasColumnName("part_type");

            //builder.HasMany(p => p.Attachments)
            //.WithOne()
            //.HasForeignKey(al => al.EntityId)
            //.HasConstraintName("FK_Part_AttachmentLink");

        }
    }
}
