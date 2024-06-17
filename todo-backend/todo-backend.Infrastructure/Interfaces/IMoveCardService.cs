namespace todo_backend.Infrastructure.Interfaces
{
    public interface IMoveCardService
    {
        Task MoveCard(int cardId, int catalogId_1, int catalogId_2, int boardId);
    }
}
