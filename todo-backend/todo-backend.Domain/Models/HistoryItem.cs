using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo_backend.Domain.Models
{
    public class HistoryItem : Dbitem
    {
        public DateTime Timesetup { get; set; }
        public string EventDescription { get; set; }
        public int? CardId { get; set; }
        
        public HistoryItem() {
            Timesetup = DateTime.UtcNow;
        }
    }
}
