using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_backend.Domain.Models
{
    public class Board : Dbitem
    {
        public string Title { get; set; }
        public List<int> CatalogsId { get; set; }
    }
}
