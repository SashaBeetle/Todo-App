using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public CardController(IDbEntityService<Card> cardService, IDbEntityService<Catalog> catalogService)
        {
            _cardService = cardService;
            _catalogService = catalogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(Card card, int listId)
        {
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

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCard(int id, Card card)
        {
            if (id != card.Id)
                return BadRequest();

            Card updatedCard = await _cardService.Update(card);

            return Ok(updatedCard);
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

            return Ok(card);
        }
        
    }
}
