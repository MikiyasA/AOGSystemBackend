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
    public class OfferEntityTypeConfig : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("offers", AOGSystemContext.DefaultSchema);
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
            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired();
            builder.Property(x => x.BasePrice)
                .HasColumnName("base_price")
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
            builder.Property(x => x.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

        }
    }
}
