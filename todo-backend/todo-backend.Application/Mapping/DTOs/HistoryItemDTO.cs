namespace todo_backend.Application.Mapping.DTOs
{
    public class HistoryItemDTO
    {
        public int Id { get; set; }
        public DateTime Timesetup { get; set; }
        public string EventDescription { get; set; }
        public int? CardId { get; set; }
    }
}
