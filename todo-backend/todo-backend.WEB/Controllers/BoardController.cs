﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using todo_backend.Infrastructure.Interfaces;
using todo_backend.WEB.Mapping.DTOs;
using AutoMapper;

namespace todo_backend.WEB.Controllers
{
    [Route("api/Boards")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        IDbEntityService<Board> _boardService;
        IDbEntityService<Catalog> _catalogService;
        IDbEntityService<Card> _cardService;
        IDbEntityService<HistoryItem> _historyItemService;
        IMapper _mapper;
        public BoardController(
            IDbEntityService<Board> boardService, 
            IMapper mapper, 
            IDbEntityService<Catalog> catalogService, 
            IDbEntityService<Card> cardService,
            IDbEntityService<HistoryItem> historyItemService
            )
        {
            _boardService = boardService;
            _mapper = mapper;
            _catalogService = catalogService;
            _cardService = cardService;
            _historyItemService = historyItemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard(BoardDTO boardDto)
        {
            Board createdBoard = await _boardService.Create(_mapper.Map<Board>(boardDto));

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Board ◉ {createdBoard.Title} created",
                BoardId = createdBoard.Id
            });

            BoardDTO createdBoardDto = _mapper.Map<BoardDTO>(createdBoard);
            return CreatedAtAction(nameof(GetBoardById), new { id = createdBoardDto.Id }, createdBoardDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            List<Board> boards = await _boardService.GetAll().ToListAsync();
            return Ok(_mapper.Map<List<BoardDTO>>(boards));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardById(int id)
        {
            Board board = await _boardService.GetById(id);
            if (board == null)
                return NotFound();

            return Ok(_mapper.Map<BoardDTO>(board));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            Board? board = await _boardService.GetById(id);

            if (board == null)
                return NotFound();


            foreach (var el in board.CatalogsId)
            {
                Catalog catalog = await _catalogService.GetById(el);

                if (catalog != null)
                {
                    foreach (var cardId in catalog.CardsId)
                    {
                        Card card = await _cardService.GetById(cardId);

                        if (card != null)
                            await _cardService.Delete(card);
                    }

                    await _catalogService.Delete(catalog);

                }

            }

            await _boardService.Delete(board);

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBoard(int id, string title)
        {
            Board board = await _boardService.GetById(id);
            
            if (board == null)
                return NotFound();

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Board title updated from ◉ {board.Title} to ◉ {title}",
                BoardId = board.Id
            });

            board.Title = title;

            
            await _boardService.Update(board);

            return NoContent();
        }
    }
}
