using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
    }
}
