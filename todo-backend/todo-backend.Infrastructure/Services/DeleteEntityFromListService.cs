using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_backend.Infrastructure.Services
{
    public class DeleteEntityFromListService
    {
        public List<int> DeleteEntitysFromList(List<int> list, int id)
        {
            foreach (var item in list)
            {
                    list.Remove(item);
            }
            
            return list;
        }
    }
}
