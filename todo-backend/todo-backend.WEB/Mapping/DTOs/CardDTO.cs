namespace todo_backend.WEB.Mapping.DTOs
{
    public class CardDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
}
