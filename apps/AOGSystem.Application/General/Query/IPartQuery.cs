using AOGSystem.Application.General.Query.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query
{
    public interface IPartQuery
    {
        Task<PartQueryModel> GetPartByIdAsync(int id);
        Task<PartQueryModel> GetPartByPNAsync(string pNo);
    }
}
