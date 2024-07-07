using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;

namespace todo_backend.Infrastructure.Interfaces
{
    public interface IHistoryItemRepository
    {
        Task<HistoryItem> GetHistoryItemAsync(int historyItemId);
        Task<IList<HistoryItem>> GetHistoryItemsAsync();
        Task<IList<HistoryItem>> GetHistoryItemsForCardByIdAsync(int cardId);
        Task<IList<HistoryItem>> GetHistoryItemsForBoardByIdAsync(int boardId);
        Task<HistoryItem> CreateHistoryItemAsync(HistoryItem historyItem);
        Task DeleteHistoryItemAsync(int historyItem);
    }
}
