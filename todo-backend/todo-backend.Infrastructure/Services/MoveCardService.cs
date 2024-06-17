using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Services
{
    public class MoveCardService : IMoveCardService
    {
        private readonly IDbEntityService<Catalog> _catalogService;
        private readonly IDbEntityService<Card> _cardService;
        private readonly IDbEntityService<HistoryItem> _historyItemService;

        public MoveCardService(
            IDbEntityService<Catalog> catalogService, 
            IDbEntityService<Card> cardService, 
            IDbEntityService<HistoryItem> historyItemService)
        {
            _catalogService = catalogService;
            _cardService = cardService;
            _historyItemService = historyItemService;
        }
        public async Task MoveCard(int cardId, int sourceCatalogId, int destinationCatalogId, int boardId)
        {
            var sourceCatalog = await GetCatalogById(sourceCatalogId);
            var destinationCatalog = await GetCatalogById(destinationCatalogId);

            EnsureCatalogExists(sourceCatalog, "Source catalog not found");
            EnsureCatalogExists(destinationCatalog, "Destination catalog not found");

            var card = await GetCardById(cardId);
            MoveCardBetweenCatalogs(cardId, sourceCatalog, destinationCatalog);
            await LogHistoryItem(card, sourceCatalog, destinationCatalog, boardId);

            await UpdateCatalogs(sourceCatalog, destinationCatalog);
        }

        private async Task<Catalog> GetCatalogById(int catalogId)
        {
            return await _catalogService.GetById(catalogId);
        }

        private async Task<Card> GetCardById(int cardId)
        {
            var card = await _cardService.GetById(cardId);
            if (card == null)
            {
                throw new ArgumentException("Card not found");
            }
            return card;
        }

        private void EnsureCatalogExists(Catalog catalog, string errorMessage)
        {
            if (catalog == null)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        private void MoveCardBetweenCatalogs(int cardId, Catalog sourceCatalog, Catalog destinationCatalog)
        {
            if (!sourceCatalog.CardsId.Contains(cardId))
            {
                throw new ArgumentException("Card not found in source catalog");
            }

            sourceCatalog.CardsId.Remove(cardId);
            destinationCatalog.CardsId.Add(cardId);
        }

        private async Task LogHistoryItem(Card card, Catalog sourceCatalog, Catalog destinationCatalog, int boardId)
        {
            var historyItem = new HistoryItem
            {
                EventDescription = $"Card ◉ {card.Title} moved from ◉ {sourceCatalog.Title} to ◉ {destinationCatalog.Title}",
                CardId = card.Id,
                BoardId = boardId
            };
            await _historyItemService.Create(historyItem);
        }

        private async Task UpdateCatalogs(Catalog sourceCatalog, Catalog destinationCatalog)
        {
            await _catalogService.Update(sourceCatalog);
            await _catalogService.Update(destinationCatalog);
        }
    }
}