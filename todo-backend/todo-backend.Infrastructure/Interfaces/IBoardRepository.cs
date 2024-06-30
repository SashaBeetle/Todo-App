using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;

namespace todo_backend.Infrastructure.Interfaces
{
    public interface IBoardRepository
    {
        Task<Board> GetBoardByIdAsync(int boardId);
        Task<IList<Board>> GetBoardsAsync();
        Task<Board> CreateBoardAsync(Board board);
        Task<Board> UpdateBoardAsync(int id, string boardTitle);
        Task DeleteBoardAsync(int boardId);
    }
}
