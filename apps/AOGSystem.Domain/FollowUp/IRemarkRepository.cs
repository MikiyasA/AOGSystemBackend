using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public interface IRemarkRepository
    {
        Remark Add(Remark remark);
        void Update(Remark remark);
        void Delete(int id);
        Task<Remark> GetRemarkAsync(int id);
    }
}
