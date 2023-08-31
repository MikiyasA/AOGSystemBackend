using AOGSystem.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.General
{
    public class CompanyEntityTypeConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies", AOGSystemContext.DefaultSchema);
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

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(x => x.Address)
                .HasColumnName("address")
                .IsRequired();

            builder.Property(x => x.City)
                .HasColumnName("city");

            builder.Property(x => x.Country)
                .HasColumnName("country")
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasColumnName("phone");

            builder.Property(x => x.ShipToAddress)
                .HasColumnName("ship_to_address");

            builder.Property(x => x.BillToAddress)
                .HasColumnName("bill_to_address");

            builder.Property(x => x.PaymentTerm)
                .HasColumnName("payment_term");
        }
    }
}
