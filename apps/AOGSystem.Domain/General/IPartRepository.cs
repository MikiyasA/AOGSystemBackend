using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public interface IPartRepository
    {
        Part Add(Part part);
        void Update(Part part);
        void Delete(int id);
        Task<List<Part>> GetAllParts();
        Task<Part> GetPartByIDAsync(int id);
        Task<Part> GetPartByPNAsync(string partNo);
        List<Part> GetPartByPartialPN(string partNo);
        List<Part> GetPartByManufacturer(string manufacturer);
        List<Part> GetPartByType(string type);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);


    }
}
