using AutoMapper;
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
        private readonly IMapper _mapper;

        public HistoryItemController(
            IDbEntityService<HistoryItem> historyItemService,
            IDbEntityService<Card> cardService,
            IMapper mapper)
        {
            _historyItemService = historyItemService;
            _cardService = cardService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHistoryItems()
        {
            List<HistoryItem> historyItems = await _historyItemService.GetAll().ToListAsync();

           
            return Ok(_mapper.Map<List<HistoryItem>>(historyItems));
        }

        [HttpGet("ForCard{id}")]
        public async Task<IActionResult> GetAllHistoryItemsForCard(int id)
        {
            Card card = await _cardService.GetById(id);

            if (card == null)
                return NotFound();

            List<HistoryItem> historyItems = await _historyItemService.GetAll().Where(h => h.CardId == id).ToListAsync();

            return Ok(_mapper.Map<List<HistoryItem>>(historyItems));
        } 
    }
}
