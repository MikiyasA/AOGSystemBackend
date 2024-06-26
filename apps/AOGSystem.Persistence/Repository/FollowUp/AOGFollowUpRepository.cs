﻿using AOGSystem.Domain.FollowUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AOGSystem.Domain;

namespace AOGSystem.Persistence.Repository.FollowUp
{
    public class AOGFollowUpRepository : IAOGFollowUpRepository
    {
        private readonly AOGSystemContext _context;

        public AOGFollowUpRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public AOGFollowUp Add(AOGFollowUp AOGFollowUp)
        {
            return _context.AOGFollowUps.Add(AOGFollowUp).Entity;
        }

        public async void Delete(Guid id)
        {
            _context.Remove(_context.AOGFollowUps.FindAsync(id).Result);
        }

        public async Task<List<AOGFollowUp>> GetAllActiveFollowUpAsync()
        {
            var followUp = await _context.AOGFollowUps
                .Where(x => x.Status != "Closed")
                .OrderByDescending(x => x.RequestDate)
                .ToListAsync();
            foreach (var fp in followUp)
            {
                await _context.Entry(fp)
                    .Collection(x => x.Remarks)
                    .LoadAsync();
            }
            return followUp;
        }

        public async Task<List<AOGFollowUp>> GetAllActiveFollowUpByTabIdAsync(Guid id)
        {
            var followUp = await _context.AOGFollowUps
                    .Where(x => x.FollowUpTabsId == id)
                    .OrderByDescending(x => x.RequestDate)
                    .ToListAsync();

            foreach (var fu in followUp)
            {
                await _context.Entry(fu)
                    .Collection(x => x.Remarks)
                    .Query()
                    .OrderByDescending(x => x.CreatedAT)
                    .ThenByDescending(x => x.UpdatedAT)
                    .LoadAsync();
            }

            return followUp;
        }

        public async Task<PaginatedList<AOGFollowUp>> GetAllAOGFollowUpAsync(Expression<Func<AOGFollowUp, bool>> predicate, int page, int pageSize)
        {
            IQueryable<AOGFollowUp> query = _context.AOGFollowUps;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await PaginatedList<AOGFollowUp>.ToPagedList(query.OrderByDescending(x => x.CreatedAT), page, pageSize);
            return result;
        }

        public async Task<AOGFollowUp> GetAOGFollowUpByIDAsync(Guid id)
        {
            var followUp = await _context.AOGFollowUps.FindAsync(id);
            if(followUp != null)
            {
                await _context.Entry(followUp).Collection(x => x.Remarks)
                   .LoadAsync();
            }
            return followUp;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            //AddAuditInfo(userId);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(AOGFollowUp aOGFollowUp)
        {
            _context.Entry(aOGFollowUp).State = EntityState.Modified;
        }
    }
}
