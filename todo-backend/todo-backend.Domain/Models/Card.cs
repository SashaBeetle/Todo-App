using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_backend.Domain.Models
{
    public class Card : Dbitem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}
