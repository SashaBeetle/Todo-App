using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_backend.Domain.Models;

namespace todo_backend.Infrastructure.Interfaces
{
    public interface ICardRepository
    {
        Task<Card> GetCardByIdAsync(int cardId);
        Task<IList<Card>> GetCardsAsync();
        Task<Card> CreateCardAsync(Card card);
        Task<Card> UpdateCardAsync(Card card);
        Task DeleteCardByIdAsync(int cardId);
    }
}
