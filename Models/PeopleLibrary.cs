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

    public async Task<IEnumerable<Card>> GetAllCards(int rows)
    {            
        return await _cardRepository.GetAllCards(rows);
    }

    public async Task<IEnumerable<Card>> GetFilteredCards(Card cardFilter)
    {
        return await _cardRepository.GetFilteredCards(cardFilter);
    }       

    public async Task CreateCards(List<Card> cards)
    {
        await _cardRepository.CreateCards(cards);
    }

    public bool IsLibraryEmpty()
    { 
        return _cardRepository.IsLibraryEmpty();
    }
}
