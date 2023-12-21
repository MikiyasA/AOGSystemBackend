using AOGSystem.Domain.FollowUp;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.FollowUp
{
    public class FollowUpTabsRepository : IFollowUpTabsRepository
    {
        private readonly AOGSystemContext _context;

        public FollowUpTabsRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public FollowUpTabs Add(FollowUpTabs FollowUpTabs)
        {
            return _context.FollowUpTabs.Add(FollowUpTabs).Entity;
        }

        public async void Delete(int id)
        {
            _context.Remove(_context.FollowUpTabs.FindAsync(id).Result);
        }

        public async Task<List<FollowUpTabs>> GetAllActiveFollowUpTabsAsync()
        {
            var followUp = await _context.FollowUpTabs
                .Where(x => x.Status == "Active")
                //.OrderByDescending(x => x.CreatedAT)
                .ToListAsync();
            foreach (var fp in followUp)
            {
                await _context.Entry(fp)
                    .Collection(x => x.FollowUps)
                    .LoadAsync();
            }
            //var followUp = await _context.FollowUpTabss
            //    .Where(x => x.Status != "Closed")
            //    .Include(x => x.Remarks)  // Include Remarks collection
            //    .Include(x => x.Part)       // Include PN collection
            //    .ToListAsync();
            return followUp;
        }

        public Task<List<FollowUpTabs>> GetAllFollowUpTabsAsync()
        {
            return _context.FollowUpTabs.ToListAsync();
        }

        public async Task<FollowUpTabs> GetFollowUpTabsByIDAsync(int id)
        {
            var followUp = await _context.FollowUpTabs.FindAsync(id);
            //if(followUp != null)
            //{
            //    await _context.Entry(followUp).Collection(x => x.FollowUps)
            //       .LoadAsync();
            //}
            return followUp;
        }

        public async Task<FollowUpTabs> GetFollowUpTabsByNameAsync(string name)
        {
            try
            {
                var followUp = await _context.FollowUpTabs.FirstOrDefaultAsync(x => x.Name == name);
                return followUp;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine(ex.Message);
                throw; // Rethrow the exception
            }
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            //AddAuditInfo(userId);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(FollowUpTabs FollowUpTabs)
        {
            _context.Entry(FollowUpTabs).State = EntityState.Modified;
        }
    }
}
