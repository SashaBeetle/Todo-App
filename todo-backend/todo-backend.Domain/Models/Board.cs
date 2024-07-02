namespace todo_backend.Domain.Models
{
    public class Board : Dbitem
    {
        public string Title { get; set; }
        public ICollection<Catalog>? Catalogs { get; set; }
    }
}
