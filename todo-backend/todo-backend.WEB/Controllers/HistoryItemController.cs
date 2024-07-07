using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Mapping.DTOs;


namespace todo_backend.WEB.Controllers
{
    [Route("api/v1/historyitems")]
    [ApiController]
    public class HistoryItemController : ControllerBase
    {
        private readonly IHistoryItemRepository _historyItemRepository;
        private readonly IMapper _mapper;

        public HistoryItemController(IHistoryItemRepository historyRepository, IMapper mapper)
        {
            _historyItemRepository = historyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHistoryItems()
        {
            IList<HistoryItem> historyItems = await _historyItemRepository.GetHistoryItemsAsync();
  
            return Ok(_mapper.Map<List<HistoryItemDTO>>(historyItems));
        }

        [HttpGet("card/{id}")]
        public async Task<IActionResult> GetAllHistoryItemsForCard(int id)
        {
            IList<HistoryItem> historyItemsForCard = await _historyItemRepository.GetHistoryItemsForCardByIdAsync(id);

            if (historyItemsForCard == null)
                return NotFound();

            return Ok(_mapper.Map<List<HistoryItemDTO>>(historyItemsForCard));
        }

        [HttpGet("board/{id}")]
        public async Task<IActionResult> GetHistoryItemForBoard(int id)
        {
            IList<HistoryItem> historyItemsForCard = await _historyItemRepository.GetHistoryItemsForBoardByIdAsync(id);

            if (historyItemsForCard == null)
                return NotFound();

            return Ok(_mapper.Map<List<HistoryItemDTO>>(historyItemsForCard));
        }
    }
}
