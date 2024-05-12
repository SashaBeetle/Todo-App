namespace todo_backend.WEB.Mapping.DTOs
{
    public class BoardDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> CatalogsId { get; set; }
    }
}
