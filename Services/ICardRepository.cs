using System.Collections.Generic;
using System.Threading.Tasks;
using Task_2.Model;

namespace Task_2.Services;

public interface ICardRepository
{    
    Task AsyncCreateCards(List<Card> cards);        
    Task<IEnumerable<Card>> AsyncGetAllCards(int rows);
    Task<IEnumerable<Card>> AsyncGetFilteredCards(Card cardFilter);        
    bool IsLibraryEmpty();
}
