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
            card.History = new List<HistoryItem>();

            card.History.Add(new HistoryItem { EventDescription = $"Card ◉ {card.Title} created" });
            
           

            Card createdCard = await _cardService.Create(card);
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

            HistoryItem history = new HistoryItem {
                CardId=card.Id,
                EventDescription = $"Card ◉ {card.Title} Deleted" 
            };
            await _historyItemService.Create(history);

            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCard(Card card)
        {
            card.DueDate = card.DueDate.ToUniversalTime();

            await _cardService.Update(card);

            card.History.Add(new HistoryItem { EventDescription = $"Card ◉ {card.Title} Updated" });

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
