using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_backend.Domain.Models
{
    public class Catalog : Dbitem
    {
        public string Title { get; set; }
        public List<int> CardsId { get; set; }
        public virtual List<Card> Cards { get; set; }
        
    }
}
