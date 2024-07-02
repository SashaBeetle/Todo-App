using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly TodoDbContext _dbContext;
        private readonly IDbEntityService<Board> _dbBoardService;
        private readonly IDbEntityService<HistoryItem> _historyItemService;
        public BoardRepository(TodoDbContext dbContext, IDbEntityService<Board> dbBoardService, IDbEntityService<HistoryItem> historyItemService)
        {
            _dbContext = dbContext;
            _dbBoardService = dbBoardService;
            _historyItemService = historyItemService;
        }
        public async Task<Board> CreateBoardAsync(Board board)
        {
            Board createdBoard = await _dbBoardService.Create(board);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Board ◉ {createdBoard.Title} created",
                BoardId = createdBoard.Id
            });

            return createdBoard;
        }

        public async Task DeleteBoardAsync(int boardId)
        {
            Board board = await _dbBoardService.GetById(boardId);

            await _dbBoardService.Delete(board);
        }

        public async Task<Board> GetBoardByIdAsync(int boardId)
        {
            var board = await _dbContext.Set<Board>()
             .Include(o => o.Catalogs!).ThenInclude(c => c.Cards!)
             .FirstOrDefaultAsync(o => o.Id == boardId) 
             ?? throw new Exception($"Order {boardId} not found");

            return board;
        }

        public async Task<IList<Board>> GetBoardsAsync()
        {
            var boards = await _dbContext.Set<Board>()
             .Include(o => o.Catalogs!)
             .OrderBy(o => o.Id).ToListAsync()
             ?? throw new Exception($"Orders not found");

            return boards;
        }

        public async Task<Board> UpdateBoardAsync(int id, string boardTitle)
        {
            Board board = await _dbBoardService.GetById(id);

            await _historyItemService.Create(new HistoryItem()
            {
                EventDescription = $"Board title updated from ◉ {board.Title} to ◉ {boardTitle}",
                BoardId = board.Id
            });

            board.Title = boardTitle;

            await _dbBoardService.Update(board);

            return board;
        }
    }
}
