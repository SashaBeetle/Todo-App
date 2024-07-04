using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Mapping.DTOs;

namespace todo_backend.WEB.Controllers
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IDbEntityService<HistoryItem> _historyItemService;
        private readonly IMapper _mapper;

        public CardController(
            ICardRepository cardRepository,
            IDbEntityService<HistoryItem> historyItemService, 
            IMapper mapper
            )
        {
            _cardRepository = cardRepository;
            _historyItemService = historyItemService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCard(CardDTO cardDto, [Required] int boardId)
        {
            cardDto.DueDate = cardDto.DueDate.ToUniversalTime();

            Card createdCard = await _cardRepository.CreateCardAsync(_mapper.Map<Card>(cardDto));

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Card ◉ {createdCard.Title} created",
                CardId = createdCard.Id,
                BoardId = boardId
            });

            CardDTO createdCardDto = _mapper.Map<CardDTO>(createdCard);

            return CreatedAtAction(nameof(GetCardById), new { id = createdCardDto.Id }, createdCardDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            IList<Card> cards = await _cardRepository.GetCardsAsync();

            return Ok(_mapper.Map<List<CardDTO>>(cards));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardById(int id)
        {
            Card card = await _cardRepository.GetCardByIdAsync(id);

            if (card == null)
                return NotFound();

            card.DueDate = card.DueDate.ToLocalTime();


            return Ok(_mapper.Map<CardDTO>(card));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateCard(CardDTO cardDto)
        {
            Card card = _mapper.Map<Card>(cardDto);
            card.DueDate = card.DueDate.ToUniversalTime();

            Card updatedCard = await _cardRepository.UpdateCardAsync(card);

            return Ok(_mapper.Map<CardDTO>(updatedCard));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            await _cardRepository.DeleteCardByIdAsync(id);

            return NoContent();
        }
    }
}
