using AOGSystem.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AOGSystem.Persistence.EntityConfigurations.Sales
{
    public class SalesEntityTypeConfig : IEntityTypeConfiguration<AOGSystem.Domain.Sales.Sales>
    {
        public void Configure(EntityTypeBuilder<Domain.Sales.Sales> builder)
        {
            builder.ToTable("sales", AOGSystemContext.DefaultSchema);
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

            builder.Property(x => x.CompanyId)
                .HasColumnName("company_id")
                .IsRequired();

            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.OrderByName)
                .HasColumnName("ordered_by_name")
                .IsRequired(false);

            builder.Property(x => x.OrderByEmail)
                .HasColumnName("order_by_email")
                .IsRequired(false);

            builder.Property(x => x.OrderNo)
                .HasColumnName("order_no")
                .IsRequired();

            builder.Property(x => x.CustomerOrderNo)
                .HasColumnName("customer_order_no")
                .IsRequired(false);

            builder.Property(x => x.ShipToAddress)
                .HasColumnName("ship_to_address")
                .IsRequired(false);
            builder.Property(x => x.Status)
                .HasJsonPropertyName("status")
                .IsRequired();

            builder.Property(x => x.Note)
                .HasColumnName("note")
                .IsRequired(false);

            builder.Property(x => x.IsApproved)
                .HasColumnName("is_approve");

            builder.Property(x => x.IsFullyShipped)
                .HasColumnName("is_fully_shipped");

            builder.Property(x => x.AWBNo)
                .HasColumnName("awb_no")
                .IsRequired(false);

            builder.Property(x => x.ShipDate)
                .HasColumnName("ship_date")
                .IsRequired(false);

            builder.Property(x => x.ReceivedByCustomer)
                .HasColumnName("received_bu_customer");

            builder.Property(x => x.ReceivedDate)
                .HasColumnName("received_date")
                .IsRequired(false);

        }
    }
}
