using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public interface IJwtService
    {
        string GenerateToken(string userId, IEnumerable<Claim> claims);
    }
}
