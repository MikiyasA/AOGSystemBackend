using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query
{
    public class PartQueryMSql 
    {
        private readonly IPartRepository _partRepository;
        public PartQueryMSql(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        
    }
}
