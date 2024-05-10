namespace todo_backend.WEB.Mapping.DTOs
{
    public class CatalogDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> CardsId { get; set; }
    }
}
