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

namespace AOGSystem.Persistence
{
    public class AOGSystemContext : DbContext
    {
        public const string DefaultSchema = "AOGsystem";

        public DbSet<Company> Companies { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationPartList> QuotationPartLists { get; set; }

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
            // Add other entity configurations here...
        }

        public async Task<int> SaveEntitiesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            // await _mediator?.DispatchDomainEventsAsync(this);

            AddAuditInfo(userId);
            return await SaveChangesAsync(cancellationToken);
        }

        public int SaveChanges(string userId = null)
        {
            AddAuditInfo(userId);
            return base.SaveChanges();
        }

        private void AddAuditInfo(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return;

            // Add auditing information if needed (CreatedBy, CreatedDate, UpdatedBy, UpdatedDate).
            // This code is commented, but you can uncomment and adapt it as required.
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction ??= await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                _currentTransaction?.Dispose();
            }
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
