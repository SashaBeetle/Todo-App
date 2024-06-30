namespace todo_backend.Domain.Models
{
    public class Catalog : Dbitem
    {
        public string Title { get; set; }
        public int BoardId { get; set; }
        public Board? Board { get; set; }
        public ICollection<Card>? Cards { get; set; }
    }
}
