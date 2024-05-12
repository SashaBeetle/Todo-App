using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend.WEB.Controllers
{
    [Route("api/catalog")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IDbEntityService<Catalog> _catalogService;
        private readonly IDbEntityService<Card> _cardService;
        private readonly IDbEntityService<HistoryItem> _historyItemService;
        private readonly IDbEntityService<Board> _boardService;
        private readonly IMoveCardService _moveCardService;
        private readonly IMapper _mapper;

        public CatalogController(
            IDbEntityService<Catalog> catalogService, 
            IMoveCardService moveCardService, 
            IDbEntityService<Card> cardService,
            IDbEntityService<HistoryItem> historyItemService,
            IDbEntityService<Board> boardService,
            IMapper mapper
            )
        {
            _moveCardService = moveCardService;
            _catalogService = catalogService;
            _cardService = cardService;
            _historyItemService = historyItemService;
            _mapper = mapper;
            _boardService = boardService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCatalog(CatalogDTO catalogDto, [Required] int BoardId)
        {
            Catalog createdCatalog = await _catalogService.Create(_mapper.Map<Catalog>(catalogDto));

            Board board = await _boardService.GetById(BoardId);
            board.CatalogsId.Add(createdCatalog.Id);
            await _boardService.Update(board);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Catalog ◉ {createdCatalog.Title} created",
            });

            CatalogDTO createdCatalogDto = _mapper.Map<CatalogDTO>(createdCatalog);
            return CreatedAtAction(nameof(GetCatalogsById), new { id = createdCatalogDto.Id }, createdCatalogDto);
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

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Catalog ◉ {catalog.Title} deleted",
            });

            await _catalogService.Delete(catalog);

            

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCatalogs()
        {
            List<Catalog> catalogs = await _catalogService.GetAll().ToListAsync();

            
            return Ok(_mapper.Map<List<CatalogDTO>>(catalogs));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogsById(int id)
        {
            Catalog? catalog = await _catalogService.GetById(id);

            if (catalog == null)
                return NotFound();

            return Ok(_mapper.Map<CatalogDTO>(catalog));
        }
        [HttpGet("ForBoard/{id}")]
        public async Task<IActionResult> GetCatalogsByBoardId(int id)
        {
            Board board = await _boardService.GetById(id);

            if (board == null)
                return NotFound();

            List<Catalog> catalogs = new List<Catalog>();

            foreach (var catalogId in board.CatalogsId)
            {
                Catalog catalog =  await _catalogService.GetById(catalogId);
                catalogs.Add(catalog);
            }

            return Ok(_mapper.Map<List<CatalogDTO>>(catalogs));
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCatalog(int id, string title)
        {
            Catalog? catalog = await _catalogService.GetById(id);
            
            if (catalog == null)
                return NotFound();

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Catalog ◉ {catalog.Title} renamed to ◉ {title}",
            });


            catalog.Title = title;
            Catalog updatedCatalog = await _catalogService.Update(catalog);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Catalog ◉ {catalog.Title} renamed to ◉ {updatedCatalog.Title}",
            });

            return Ok(_mapper.Map<CatalogDTO>(updatedCatalog));
        }

        [HttpPatch("MoveCard")]
        public async Task<IActionResult> MoveCardBetweenCatalogs(int catalogId_1, int catalogId_2, int cardId)
        {
            await _moveCardService.MoveCard(cardId, catalogId_1, catalogId_2);

            return Ok();
        }

    }
}
