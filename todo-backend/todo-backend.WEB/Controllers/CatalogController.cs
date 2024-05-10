using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.WEB.Controllers
{
    [Route("api/catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IDbEntityService<Catalog> _catalogService;
        private readonly IDbEntityService<Card> _cardService;
        private readonly IMoveCardService _moveCardService;

        public CatalogController(IDbEntityService<Catalog> catalogService, IMoveCardService moveCardService, IDbEntityService<Card> cardService)
        {
            _moveCardService = moveCardService;
            _catalogService = catalogService;
            _cardService = cardService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCatalog(Catalog catalog)
        {
            Catalog createdCatalog = await _catalogService.Create(catalog);

            return CreatedAtAction(nameof(GetCatalogsById), new { id = createdCatalog.Id }, createdCatalog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog(int id)
        {
            Catalog? catalog = await _catalogService.GetById(id);

            if (catalog == null)
                return NotFound();


            foreach (var cardId in catalog.CardsId)
            {
                Card? card = await _cardService.GetById(cardId);
                if (card != null)
                    await _cardService.Delete(card);
            }

            await _catalogService.Delete(catalog);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCatalogs()
        {
            List<Catalog> catalogs = await _catalogService.GetAll().ToListAsync();

            return Ok(catalogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogsById(int id)
        {
            Catalog? catalog = await _catalogService.GetById(id);

            if (catalog == null)
                return NotFound();

            return Ok(catalog);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCatalog(int id, string title)
        {
            Catalog? catalog = await _catalogService.GetById(id);
            
            if (catalog == null)
                return NotFound();

            catalog.Title = title;

            Catalog updatedCatalog = await _catalogService.Update(catalog);

            return Ok(updatedCatalog);
        }

        [HttpPatch("MoveCard")]
        public async Task<IActionResult> MoveCardBetweenCatalogs(int catalogId_1, int catalogId_2, int cardId)
        {
            await _moveCardService.MoveCard(cardId, catalogId_1, catalogId_2);

            return Ok();
        }

    }
}
