using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend.WEB.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly IDbEntityService<Card> _cardService;
        private readonly IDbEntityService<Catalog> _catalogService;
        private readonly IDbEntityService<HistoryItem> _historyItemService;
        private readonly IMapper _mapper;

        public CardController(
            IDbEntityService<Card> cardService, 
            IDbEntityService<Catalog> catalogService,
            IDbEntityService<HistoryItem> historyItemService, 
            IMapper mapper
            )
        {
            _cardService = cardService;
            _catalogService = catalogService;
            _historyItemService = historyItemService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(CardDTO cardDto, int listId, int boardId)
        {
            Card card = _mapper.Map<Card>(cardDto);
            card.DueDate = card.DueDate.ToUniversalTime();


            Card createdCard = await _cardService.Create(card);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} created",
                CardId = createdCard.Id,
                BoardId = boardId
            });

            Catalog existedCatalog = await _catalogService.GetById(listId);

            await _cardService.AddCardToCatalog(existedCatalog, createdCard.Id);


            CardDTO createdCardDto = _mapper.Map<CardDTO>(createdCard);

            return CreatedAtAction(nameof(GetCardById), new { id = createdCardDto.Id }, createdCardDto);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id, int boardId)
        {
            Card? card = await _cardService.GetById(id);

            if (card == null)
                return NotFound();

            await _cardService.DeleteCardFromCatalogs(id);
            await _cardService.Delete(card);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} deleted",
                CardId = card.Id,
                BoardId = boardId
            });

            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCard(CardDTO cardDto, int boardId)
        {
            Card card = _mapper.Map<Card>(cardDto);
            card.DueDate = card.DueDate.ToUniversalTime();

            await _cardService.Update(card);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {card.Title} updated",
                CardId = card.Id,
                BoardId = boardId
            });

            return Ok(_mapper.Map<CardDTO>(card));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            List<Card> cards = await _cardService.GetAll().ToListAsync();


            return Ok(_mapper.Map<List<CardDTO>>(cards));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(int id)
        {
            Card? card = await _cardService.GetById(id);

            if (card == null)
                return NotFound();

            card.DueDate = card.DueDate.ToLocalTime();

            
            return Ok(_mapper.Map<CardDTO>(card));
        }
        
    }
}
