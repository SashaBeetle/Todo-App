using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;

namespace todo_backend.Infrastructure.Interfaces
{
    public interface ICatalogRepository
    {
        Task<Catalog> GetCatalogAsync(int catalogId);
        Task<IList<Catalog>> GetCatalogsAsync();
        Task<Catalog> CreateCatalogAsync(Catalog catalog);
        Task<Catalog> UpdateCatalogAsync(int id, string catalogTitle);
        Task DeleteCatalogAsync(int catalogId);
    }
}
