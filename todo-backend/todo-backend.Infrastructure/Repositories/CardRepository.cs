using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Repositories
{
    
    public class CardRepository : ICardRepository
    {
        private readonly TodoDbContext _dbContext;
        private readonly IDbEntityService<Card> _dbCardService;
        private readonly IDbEntityService<HistoryItem> _dbHistoryItemService;
        private readonly IDbEntityService<Catalog> _dbCatalogService;

        public CardRepository(
            TodoDbContext dbContext,
            IDbEntityService<Card> dbCardService,
            IDbEntityService<HistoryItem> dbHistoryItemService,
            IDbEntityService<Catalog> dbCatalogService)
        {
            _dbContext = dbContext;
            _dbCardService = dbCardService;
            _dbHistoryItemService = dbHistoryItemService;
            _dbCatalogService = dbCatalogService;
        }
        public async Task<Card> CreateCardAsync(Card card)
        {
            Card createdCard = await _dbCardService.Create(card);

            Catalog catalogForCard = await _dbCatalogService.GetById(card.CatalogId) ?? throw new Exception("Catalog isn't correct");

            await _dbHistoryItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {createdCard.Title} created",
                CardId = createdCard.Id,
                BoardId = catalogForCard.BoardId
            });

            return createdCard;
        }

        public async Task DeleteCardByIdAsync(int cardId)
        {
            Card card = await _dbCardService.GetById(cardId) ?? throw new Exception($"Card with Id: {cardId} not found");

            Catalog catalogForCard = await _dbCatalogService.GetById(card.CatalogId) ?? throw new Exception("Catalog isn't correct");

            HistoryItem historyItem = new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} deleted",
                CardId = card.Id,
                BoardId = catalogForCard.BoardId
            };

            await _dbCardService.Delete(card);
            await _dbHistoryItemService.Create(historyItem);
        }

        public async Task<Card> GetCardByIdAsync(int cardId)
        {
            Card card = await _dbContext.Set<Card>()
             .FirstOrDefaultAsync(o => o.Id == cardId)
             ?? throw new Exception($"Card with Id: {cardId} not found");

            return card;
        }

        public async Task<IList<Card>> GetCardsAsync()
        {
            var cards = await _dbContext.Set<Card>()
                .OrderBy(o => o.Id).ToListAsync()
                ?? throw new Exception($"Cards not found");

            return cards;
        }

        public async Task<Card> UpdateCardAsync(Card card)
        {
            Card updatedCard = await _dbCardService.GetById(card.Id) ?? throw new Exception($"Card with Id: {card.Id} not correct");

            updatedCard.Title = card.Title;
            updatedCard.Description = card.Description;
            updatedCard.Priority = card.Priority;
            updatedCard.DueDate = card.DueDate;
            updatedCard.CatalogId = card.CatalogId;

            Catalog catalogForCard = await _dbCatalogService.GetById(card.CatalogId) ?? throw new Exception("Catalog isn't correct");

            HistoryItem historyItem = new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} updated",
                CardId = card.Id,
                BoardId = catalogForCard.BoardId
            };

            await _dbCardService.Update(updatedCard);
            await _dbHistoryItemService.Create(historyItem);

            return updatedCard;
        }
    }
}
