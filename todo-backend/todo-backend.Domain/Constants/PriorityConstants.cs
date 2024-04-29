using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_backend.Domain.Constants
{
    public static class PriorityConstants
    {
        public static IEnumerable<string> GetAllPriority()
        {
            return new List<string>
            {
            "Low",
            "Medium",
            "High" 
            };
        }
    }
}
