using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using AOGSystem.Persistence.EntityConfigurations;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using AOGSystem.Persistence.EntityConfigurations.Quotation;
using AOGSystem.Persistence.EntityConfigurations.General;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Persistence.EntityConfigurations.FollowUp;
using Polly;
using AOGSystem.Persistence.EntityConfigurations.CoreFollowUps;
using AOGSystem.Domain.CoreFollowUps;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AOGSystem.Persistence.EntityConfigurations.Sales;
using AOGSystem.Domain.Sales;
using AOGSystem.Domain.Invoices;
using AOGSystem.Persistence.EntityConfigurations.Invoices;
using AOGSystem.Domain.Loans;
using AOGSystem.Persistence.EntityConfigurations.Loans;
using AOGSystem.Domain.SOA;
using AOGSystem.Persistence.EntityConfigurations.SOA;
using AOGSystem.Domain.Attachments;
using AOGSystem.Persistence.EntityConfigurations.Attachmetns;

namespace AOGSystem.Persistence
{
    public class AOGSystemContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AOGSystemContext(DbContextOptions<AOGSystemContext> options)
        : base(options) { }


        public const string DefaultSchema = "AOGsystem";

        public DbSet<Company> Companies { get; set; }
        public DbSet<Part> Parts { get; set; }

        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationPartList> QuotationPartLists { get; set; }

        public DbSet<AOGFollowUp> AOGFollowUps { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<CoreFollowUp> CoreFollowUps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FollowUpTabs> FollowUpTabs { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesPartList> SalesPartLists { get; set; }
        public DbSet<InvoicePartList> InvoicePartLists { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanPartList> LoanPartLists { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<InvoiceList> InvoiceLists { get; set; }
        public DbSet<BuyerRemark> BuyerRemarks { get; set; }
        public DbSet<FinanceRemark> FinanceRemarks { get; set; }

        public DbSet<CostSaving> CostSavings { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttachmentLink> AttachmentLinks { get; set; }



        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;

        public AOGSystemContext(DbContextOptions<AOGSystemContext> options, IMediator mediator = null)
            : base(options)
        {
            _mediator = mediator;
            Debug.WriteLine($"AOGSystemContext::ctor -> {GetHashCode()}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new PartEntityTypeConfig());

            modelBuilder.ApplyConfiguration(new QuotationEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new QuotationPartListEntityTypeConfig());

            modelBuilder.ApplyConfiguration(new AOGFollowUpEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new RemarkEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new FollowUpTabsEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new AssignmentEntityTypeConfig());

            modelBuilder.ApplyConfiguration(new CoreFollowUpEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfig());

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims"); 
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins"); 
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens"); 
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims"); 
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new InvoiceEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new SalesEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new SalesPartListEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new InvoicePartListEntityConfig());

            modelBuilder.ApplyConfiguration(new LoanEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new LoanPartListEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new OfferEntityTypeConfig());

            modelBuilder.ApplyConfiguration(new VendorEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new InvoiceEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new BuyerRemarkEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new FinanceRemarkEntityTypeConfig());

            modelBuilder.ApplyConfiguration(new CostSavingEntityTypeConfig());
            
            modelBuilder.ApplyConfiguration(new AttachmentEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new AttachmentLinkEntityTypeConfig());




        }


        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);

        }

    }

    public class AOGSystemContextFactory : IDesignTimeDbContextFactory<AOGSystemContext>
    {
        public AOGSystemContext CreateDbContext(string[] args)
        {
            // Build configuration from appsettings.json or any other configuration source you prefer.
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the connection string from your configuration.
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Create DbContextOptions with the connection string.
            DbContextOptionsBuilder<AOGSystemContext> builder = new DbContextOptionsBuilder<AOGSystemContext>();
            builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34)),
                mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)); // Adjust the version as needed.

            return new AOGSystemContext(builder.Options);
        }
    }

    public static class StringExtensions
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }
            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
