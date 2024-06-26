using todo_backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Mapping.DTOs;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace todo_backend.WEB.Controllers
{
    [Route("api/Boards")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;
        public BoardController( IBoardRepository boardRepository,IMapper mapper)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBoard(BoardDTO boardDto)
        {
            Board createdBoard = await _boardRepository.CreateBoardAsync(_mapper.Map<Board>(boardDto));

            BoardDTO createdBoardDto = _mapper.Map<BoardDTO>(createdBoard);
            return CreatedAtAction(nameof(GetBoardById), new { id = createdBoardDto.Id }, createdBoardDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            IList<Board> boards = await _boardRepository.GetBoardsAsync();

            if(boards == null)
                return NotFound();

            return Ok(_mapper.Map<List<BoardDTO>>(boards));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardById(int id)
        {
            Board board = await _boardRepository.GetBoardByIdAsync(id);

            if (board == null)
                return NotFound();

            return Ok(_mapper.Map<BoardDTO>(board));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            await _boardRepository.DeleteBoardAsync(id);

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBoard(int id, [Required] string title)
        {
            Board board = await _boardRepository.UpdateBoardAsync(id, title);
            
            if (board == null)
                return NotFound();

            return NoContent();
        }
    }
}
