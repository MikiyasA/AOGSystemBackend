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
    public class UserEntityTypeConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", AOGSystemContext.DefaultSchema);
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

            //builder.Property(x => x.Username)
            //    .HasColumnName("username")
            //    .IsUnicode()
            //    .IsRequired();

            //builder.Property(x => x.Password)
            //    .HasColumnName("password")
            //    .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email");

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number");

            builder.HasMany<Domain.Quotation.Quotation>()
                .WithOne(x => x.OfferedBy)
                .HasForeignKey(x => x.OfferedById)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
