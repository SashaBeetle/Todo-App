using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly TodoDbContext _dbContext;
        private readonly IDbEntityService<Catalog> _dbCatalogService;

        public CatalogRepository(TodoDbContext dbContext, IDbEntityService<Catalog> dbCatalogService)
        {
            _dbContext = dbContext;
            _dbCatalogService = dbCatalogService;
        }
        public async Task<Catalog> CreateCatalogAsync(Catalog catalog)
        {
            Catalog createdCatalog = await _dbCatalogService.Create(catalog);

            return createdCatalog;
        }

        public async Task DeleteCatalogAsync(int catalogId)
        {
            Catalog? catalog = await _dbCatalogService.GetById(catalogId);

            await _dbCatalogService.Delete(catalog);
        }

        public async Task<Catalog> GetCatalogAsync(int catalogId)
        {
            Catalog? catalog = await _dbContext.Set<Catalog>()
             .Include(o => o.Cards!)
             .FirstOrDefaultAsync(o => o.Id == catalogId)
             ?? throw new Exception($"Catalog with Id: {catalogId} not found");

            return catalog;
        }

        public async Task<IList<Catalog>> GetCatalogsAsync()
        {
            var boards = await _dbContext.Set<Catalog>()
            .Include(o => o.Cards!)
            .OrderBy(o => o.Id).ToListAsync()
            ?? throw new Exception($"Catalogs not found");

            return boards;
        }

        public async Task<Catalog> UpdateCatalogAsync(int id, string catalogTitle)
        {
            Catalog? catalog = await _dbCatalogService.GetById(id);

            catalog.Title = catalogTitle;

            await _dbCatalogService.Update(catalog);

            return catalog;
        }
    }
}
