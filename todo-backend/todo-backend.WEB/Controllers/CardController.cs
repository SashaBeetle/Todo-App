using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Globalization;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.WEB.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly IDbEntityService<Card> _cardService;
        private readonly IDbEntityService<Catalog> _catalogService;
        private readonly IDbEntityService<HistoryItem> _historyItemService;

        public CardController(IDbEntityService<Card> cardService, IDbEntityService<Catalog> catalogService, IDbEntityService<HistoryItem> historyItemService)
        {
            _cardService = cardService;
            _catalogService = catalogService;
            _historyItemService = historyItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(Card card, int listId)
        {
            card.DueDate = card.DueDate.ToUniversalTime();
            
            Card createdCard = await _cardService.Create(card);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} created",
                CardId = createdCard.Id
            });

            Catalog existedCatalog = await _catalogService.GetById(listId);

            await _cardService.AddCardToCatalog(existedCatalog, createdCard.Id);

            return CreatedAtAction(nameof(GetCardById), new { id = createdCard.Id }, createdCard);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            Card? card = await _cardService.GetById(id);

            if (card == null)
                return NotFound();

            await _cardService.DeleteCardFromCatalogs(id);
            await _cardService.Delete(card);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} deleted",
                CardId = card.Id
            });

            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCard(Card card)
        {
            card.DueDate = card.DueDate.ToUniversalTime();

            await _cardService.Update(card);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} updated",
                CardId = card.Id
            });

            return Ok(card);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            List<Card> cards = await _cardService.GetAll().ToListAsync();

            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(int id)
        {
            Card? card = await _cardService.GetById(id);

            if (card == null)
                return NotFound();

            card.DueDate = card.DueDate.ToLocalTime();
            return Ok(card);
        }
        
    }
}
