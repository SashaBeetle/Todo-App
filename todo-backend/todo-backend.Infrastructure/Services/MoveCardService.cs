using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;
using todo_backend.Infrastructure.Interfaces;

namespace todo_backend.Infrastructure.Services
{
    public class MoveCardService : IMoveCardService
    {
        private readonly IDbEntityService<Catalog> _catalogService;

        public MoveCardService(IDbEntityService<Catalog> catalogService)
        {
            _catalogService = catalogService;
        }
        public async Task MoveCard(int cardId, int catalogId_1, int catalogId_2)
        {
            Catalog Catalog1 = await _catalogService.GetById(catalogId_1);
            Catalog Catalog2 = await _catalogService.GetById(catalogId_2);

            if (Catalog1 == null || Catalog2 == null)
                throw new Exception("Catalog not found");

            if (Catalog1.CardsId.Contains(cardId))
            {
                Catalog1.CardsId.Remove(cardId);
                Catalog2.CardsId.Add(cardId);
            }
            else
            {
                throw new Exception("Card not found in catalog");
            }

            await _catalogService.Update(Catalog1);
            await _catalogService.Update(Catalog2);
        }
    }
}
