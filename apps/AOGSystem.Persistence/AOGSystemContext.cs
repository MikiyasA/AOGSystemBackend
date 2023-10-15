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

namespace AOGSystem.Persistence
{
    public class AOGSystemContext : DbContext
    {
        public const string DefaultSchema = "AOGsystem";

        public DbSet<Company> Companies { get; set; }
        public DbSet<Part> Parts { get; set; }

        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationPartList> QuotationPartLists { get; set; }

        public DbSet<AOGFollowUp> AOGFollowUps { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<CoreFollowUp> CoreFollowUps { get; set; }

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

            modelBuilder.ApplyConfiguration(new CoreFollowUpEntityTypeConfig());

            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles").HasKey(p => new { p.UserId, p.RoleId });
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        }
    }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);

        }

        //public async Task<int> SaveEntitiesAsync(string userId = null, CancellationToken cancellationToken = default)
        //{
        //    // await _mediator?.DispatchDomainEventsAsync(this);

        //    AddAuditInfo(userId);
        //    return await SaveChangesAsync(cancellationToken);
        //}

        //public int SaveChanges(string userId = null)
        //{
        //    AddAuditInfo(userId);
        //    return base.SaveChanges();
        //}

        //private void AddAuditInfo(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId)) return;

        //    // Add auditing information if needed (CreatedBy, CreatedDate, UpdatedBy, UpdatedDate).
        //}

        //public async Task BeginTransactionAsync()
        //{
        //    _currentTransaction ??= await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        //}

        //public async Task CommitTransactionAsync()
        //{
        //    try
        //    {
        //        await SaveChangesAsync();
        //        _currentTransaction?.Commit();
        //    }
        //    catch
        //    {
        //        RollbackTransaction();
        //        throw;
        //    }
        //    finally
        //    {
        //        _currentTransaction?.Dispose();
        //    }
        //}

        //public void RollbackTransaction()
        //{
        //    try
        //    {
        //        _currentTransaction?.Rollback();
        //    }
        //    finally
        //    {
        //        _currentTransaction?.Dispose();
        //    }
        //}
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
