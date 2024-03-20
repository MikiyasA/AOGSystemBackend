using AOGSystem.Domain.SOA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.SOA
{
    public class VendorEntityTypeConfig : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.ToTable("vendor", AOGSystemContext.DefaultSchema);
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

            builder.Property(x => x.VendorName)
                .HasColumnName("vendor_name")
                .IsRequired();
            builder.Property(x => x.VendorCode)
                .HasColumnName("vendor_code")
                .IsRequired();
            builder.Property(x => x.Address)
                .HasColumnName("address")
                .IsRequired();
            builder.Property(x => x.VendorAccountManagerName)
                .HasColumnName("vendor_account_manager_name")
                .IsRequired(false);
            builder.Property(x => x.VendorAccountManagerEmail)
                .HasColumnName("vendor_account_manager_email")
                .IsRequired(false);
            builder.Property(x => x.VendorFinanceContactName)
                .HasColumnName("vendor_finance_contact_name")
                .IsRequired(false);
            builder.Property(x => x.VendorFinanceContactEmail)
                .HasColumnName("vendor_finance_contact_email")
                .IsRequired(false);
            builder.Property(x => x.CreditLimit)
                .HasColumnName("credit_limit")
                .IsRequired(false);
            builder.Property(x => x.TotalOutstanding)
                .HasColumnName("total_outstanding")
                .IsRequired(false);
            builder.Property(x => x.UnderProcess)
                .HasColumnName("under_process")
                .IsRequired(false);
            builder.Property(x => x.UnderDispute)
                .HasColumnName("under_dispute")
                .IsRequired(false);
            builder.Property(x => x.PaidAmount)
                .HasColumnName("paid_amount")
                .IsRequired (false);
            builder.Property(x => x.ETFinanceContactName)
                .HasColumnName("et_finance_contact_name")
                .IsRequired(false);
            builder.Property(x => x.ETFinanceContactEmail)
                .HasColumnName("et_finance_contact_email")
                .IsRequired(false);
            builder.Property(x => x.SOAHandlerBuyerId)
                .HasColumnName("soa_handler_buyer_id")
                .IsRequired(false);
            builder.Property(x => x.SOAHandlerBuyerName)
                .HasColumnName("soa_handler_buyer_name")
                .IsRequired(false);
            builder.Property(x => x.CertificateExpiryDate) 
                .HasColumnName("certificate_expiry_date")
                .IsRequired(false);
            builder.Property(x => x.AssessmentDate) 
                .HasColumnName("AssessmentDate")
                .IsRequired(false);
            builder.Property(x => x.Status)
                .HasColumnName("status")
                .IsRequired();
            builder.Property(x => x.Remark)
                .HasColumnName("remark")
                .IsRequired(false);
        }
    }
}
