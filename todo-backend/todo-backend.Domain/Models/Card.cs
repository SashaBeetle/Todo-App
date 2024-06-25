namespace todo_backend.Domain.Models
{
    public class Card : Dbitem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int CatalogId { get; set; }
        public Catalog? Catalog { get; set; }
    }
}
