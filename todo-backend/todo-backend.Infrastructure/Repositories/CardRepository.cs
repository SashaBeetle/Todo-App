using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Repositories
{
    
    public class CardRepository : ICardRepository
    {
        private readonly TodoDbContext _dbContext;
        private readonly IDbEntityService<Card> _dbCardService;

        public CardRepository(TodoDbContext dbContext, IDbEntityService<Card> dbCardService)
        {
            _dbContext = dbContext;
            _dbCardService = dbCardService;
        }
        public async Task<Card> CreateCardAsync(Card card)
        {
            Card createdBoard = await _dbCardService.Create(card);

            return createdBoard;
        }

        public async Task DeleteCardByIdAsync(int cardId)
        {
            Card card = await _dbCardService.GetById(cardId);

            await _dbCardService.Delete(card);
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

            await _dbCardService.Update(updatedCard);

            return updatedCard;
        }
    }
}
