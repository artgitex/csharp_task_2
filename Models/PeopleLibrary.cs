using System.Collections.Generic;
using System.Threading.Tasks;
using Task_2.Services;

namespace Task_2.Model;

public class PeopleLibrary
{
    public string Name { get; }        
    private readonly CardRepository _cardRepository;
    
    public PeopleLibrary(string name, CardRepository cardRepository)
    {
        Name = name;
        _cardRepository = cardRepository;
    }

    public async Task<IEnumerable<Card>> AsyncGetAllCards(int rows)
    {            
        return await _cardRepository.AsyncGetAllCards(rows);
    }

    public async Task<IEnumerable<Card>> AsyncGetFilteredCards(Card cardFilter)
    {
        return await _cardRepository.AsyncGetFilteredCards(cardFilter);
    }       

    public async Task AsyncCreateCards(List<Card> cards)
    {
        await _cardRepository.AsyncCreateCards(cards);
    }

    public bool IsLibraryEmpty()
    { 
        return _cardRepository.IsLibraryEmpty();
    }
}
