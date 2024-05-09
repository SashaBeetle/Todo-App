using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryItemController : ControllerBase
    {
        private readonly IDbEntityService<HistoryItem> _historyItemService;
        private readonly IDbEntityService<Card> _cardService;

        public HistoryItemController(IDbEntityService<HistoryItem> historyItemService, IDbEntityService<Card> cardService)
        {
            _historyItemService = historyItemService;
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHistoryItems()
        {
            List<HistoryItem> historyItems = await _historyItemService.GetAll().ToListAsync();

            return Ok(historyItems);
        }

        [HttpGet("ForCard{id}")]
        public async Task<IActionResult> GetAllHistoryItemsForCard(int id)
        {
            Card card = await _cardService.GetById(id);

            if (card == null)
                return NotFound();

            List<HistoryItem> historyItems = await _historyItemService.GetAll().Where(h => h.CardId == id).ToListAsync();

            return Ok(historyItems);
        }

        [HttpPatch]
        public async Task<IActionResult> AddHistoryItem( HistoryItem historyItem)
        {
            HistoryItem createdHistoryItem = await _historyItemService.Create(historyItem);

            return CreatedAtAction(nameof(GetHistoryItemById), new { id = createdHistoryItem.Id }, createdHistoryItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoryItemById(int id)
        {
            HistoryItem? historyItem = await _historyItemService.GetById(id);

            if (historyItem == null)
                return NotFound();

            return Ok(historyItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistoryItem(HistoryItem historyItem)
        {
            HistoryItem createdHistoryItem = await _historyItemService.Create(historyItem);

            return CreatedAtAction(nameof(GetHistoryItemById), new { id = createdHistoryItem.Id }, createdHistoryItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryItem(int id)
        {
            HistoryItem? historyItem = await _historyItemService.GetById(id);

            if (historyItem == null)
                return NotFound();

            await _historyItemService.Delete(historyItem);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> AddToHistoryItem(int id, string message )
        {
            HistoryItem? historyItem = await _historyItemService.GetById(id);

            if (historyItem == null)
                return NotFound();


            HistoryItem updatedHistoryItem = await _historyItemService.Update(historyItem);

            return Ok(updatedHistoryItem);
        }
    }
}
