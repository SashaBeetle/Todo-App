using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Repositories
{
    public class HistoryItemRepository : IHistoryItemRepository
    {
        private readonly TodoDbContext _dbContext;
        private readonly IDbEntityService<HistoryItem> _dbHistoryItemService;
        public HistoryItemRepository(TodoDbContext dbContext, IDbEntityService<HistoryItem> dbHistoryItemService)
        {
            _dbContext = dbContext;
            _dbHistoryItemService = dbHistoryItemService;
        }

        public async Task<HistoryItem> CreateHistoryItemAsync(HistoryItem historyItem)
        {
            HistoryItem historyItemFromDb = await _dbHistoryItemService.Create(historyItem) ?? throw new Exception($"History not correct");

            return historyItemFromDb;
        }

        public async Task DeleteHistoryItemAsync(int historyItemId)
        {
            HistoryItem historyItemToDelete =  await _dbHistoryItemService.GetById(historyItemId) ?? throw new Exception($"History with Id: {historyItemId} not found");

            await _dbHistoryItemService.Delete(historyItemToDelete);
        }

        public async Task<HistoryItem> GetHistoryItemAsync(int historyItemId)
        {
            HistoryItem? historyItem = await _dbHistoryItemService.GetById(historyItemId) ?? throw new Exception($"History with Id: {historyItemId} not found");

            return historyItem;
        }

        public async Task<IList<HistoryItem>> GetHistoryItemsAsync()
        {
            IList<HistoryItem> historyItems = await _dbContext.Set<HistoryItem>()
                        .OrderBy(o => o.Id).ToListAsync()
                        ?? throw new Exception($"History not found");

            return historyItems;
        }

        public async Task<IList<HistoryItem>> GetHistoryItemsForCardByIdAsync(int cardId)
        {
            IList<HistoryItem> historyItems = await _dbContext.Set<HistoryItem>()
                        .Where(o => o.CardId == cardId)
                        .OrderBy(o => o.Id)
                        .ToListAsync()
                        ?? throw new Exception($"History not found");

            return historyItems;
        }

        public async Task<IList<HistoryItem>> GetHistoryItemsForBoardByIdAsync(int boardId)
        {
            IList<HistoryItem> historyItems = await _dbContext.Set<HistoryItem>()
                                    .Where(o => o.BoardId == boardId)
                                    .OrderBy(o => o.Id)
                                    .ToListAsync()
                                    ?? throw new Exception($"History not found");

            return historyItems;
        }
    }
}
