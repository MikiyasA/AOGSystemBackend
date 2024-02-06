﻿using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Sales
{
    public interface ISalePartListRepository
    {
        SalesPartList Add(SalesPartList salesPartList);
        void Update(SalesPartList salesPartList);
        void Delete(int id);
        Task<List<SalesPartList>> GetAllSalesPartLists();
        Task<SalesPartList> GetSalesPartListByIDAsync(int id);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
