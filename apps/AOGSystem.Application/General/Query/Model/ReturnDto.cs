using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query.Model
{
    public class ReturnDto<T>
    {
        public T? Data { get; set; }
        public int Count { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
