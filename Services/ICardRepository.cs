using System.Collections.Generic;
using System.Threading.Tasks;
using Task_2.Model;

namespace Task_2.Services;

public interface ICardRepository
{    
    Task CreateCards(List<Card> cards);        
    Task<IEnumerable<Card>> GetAllCards();
    Task<IEnumerable<Card>> GetFilteredCards(Card cardFilter);        
    bool IsLibraryEmpty();
}
