using AOGSystem.Domain.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.General
{
    public class UserRepository : IUserRepository
    {
        private readonly AOGSystemContext _context;
        public UserRepository(AOGSystemContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user != null)
            {
                _context.Entry(user);
            }
            return user;
        }
    }
}
