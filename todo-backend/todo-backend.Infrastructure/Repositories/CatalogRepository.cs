using Microsoft.EntityFrameworkCore;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly TodoDbContext _dbContext;
        private readonly IDbEntityService<Catalog> _dbCatalogService;
        private readonly IDbEntityService<HistoryItem> _dbHistoryItemService;

        public CatalogRepository(
            TodoDbContext dbContext, 
            IDbEntityService<Catalog> dbCatalogService, 
            IDbEntityService<HistoryItem> dbHistoryItemService)
        {
            _dbContext = dbContext;
            _dbCatalogService = dbCatalogService;
            _dbHistoryItemService = dbHistoryItemService;
        }
        public async Task<Catalog> CreateCatalogAsync(Catalog catalog)
        {
            Catalog createdCatalog = await _dbCatalogService.Create(catalog);

            await _dbHistoryItemService.Create(new HistoryItem()
            {
                EventDescription = $"Catalog ◉ {createdCatalog.Title} created",
                BoardId = createdCatalog.BoardId
            });

            return createdCatalog;
        }

        public async Task DeleteCatalogAsync(int catalogId)
        {
            Catalog catalog = await _dbCatalogService.GetById(catalogId);

            HistoryItem historyItem = new HistoryItem()
            {
                EventDescription = $"Catalog ◉ {catalog.Title} deleted",
                BoardId = catalog.BoardId
            };

            await _dbCatalogService.Delete(catalog);
            await _dbHistoryItemService.Create(historyItem);
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
            Catalog catalog = await _dbCatalogService.GetById(id);

            HistoryItem historyItem = new HistoryItem()
            {
                EventDescription = $"Catalog ◉ {catalog.Title} renamed to ◉ {catalogTitle}",
                BoardId = catalog.BoardId
            };

            catalog.Title = catalogTitle;

            await _dbCatalogService.Update(catalog);
            await _dbHistoryItemService.Create(historyItem);

            return catalog;
        }
    }
}
