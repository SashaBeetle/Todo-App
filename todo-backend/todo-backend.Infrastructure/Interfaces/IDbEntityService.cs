using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;

namespace todo_backend.Infrastructure.Interfaces
{
    public interface IDbEntityService<T> : IDisposable where T : Dbitem
    {
        Task<T?> GetById(int id);
        T GetByIdforUser(long id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);
        Task DeleteCardFromCatalogs(int id);
        Task DeleteCatalogFromBoard(Catalog catalog, int boardId);
        Task AddCardToCatalog(Catalog catalog, int cardId);
        IQueryable<T> GetAll();
    }
}
