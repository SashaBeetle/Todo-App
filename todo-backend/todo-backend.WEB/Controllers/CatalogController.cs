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

        public CatalogController(IDbEntityService<Catalog> catalogService)
        {
            _catalogService = catalogService;
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
            Catalog? catalogs = await _catalogService.GetById(id);

            if (catalogs == null)
                return NotFound();

            await _catalogService.Delete(catalogs);

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
    }
}
