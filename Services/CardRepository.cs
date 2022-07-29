using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_2.DbContexts;
using Task_2.Model;

namespace Task_2.Services;

public class CardRepository : ICardRepository
{
    public async Task CreateCards(List<Card> cards)
    {
        using (PeopleLibraryDbContext db = new PeopleLibraryDbContext())
        {
            db.Cards.AddRange(cards);
            await db.SaveChangesAsync();
        }        
    }     

    public async Task<IEnumerable<Card>> GetAllCards(int rows)
    {
        using (PeopleLibraryDbContext db = new PeopleLibraryDbContext())
        {            
                IEnumerable<Card> cards = await db.Cards.Take(rows).ToListAsync();
                return cards;
        }
    }

    public async Task<IEnumerable<Card>> GetFilteredCards(Card cardFilter)
    {
        string loadDate = cardFilter.LoadDate;
        string firstName = cardFilter.FirstName;
        string lastName = cardFilter.LastName;
        string city = cardFilter.City;
        string country = cardFilter.Country;            

        using (PeopleLibraryDbContext db = new PeopleLibraryDbContext())
        {
            IQueryable<Card> query = db.Cards;
                                            
            if (loadDate != null)
            {
                query = query.Where(r => r.LoadDate == loadDate);
            }               
            
            if (firstName != null)
            {
                query = query.Where(r => r.FirstName == firstName);
            }

            if (lastName != null)
            {
                query = query.Where(r => r.LastName == lastName);
            }

            if (city != null)
            {
                query = query.Where(r => r.City == city);
            }
           
            if (country != null)
            {
                query = query.Where(r => r.Country == country);
            }
            
            IEnumerable<Card> cards = await query.ToListAsync();

            return cards;
        }
    }

    public bool IsLibraryEmpty()
    {            
        using (PeopleLibraryDbContext db = new PeopleLibraryDbContext())
        {
            if (!db.Cards.Any())
            {
                return true;
            }
            return false;
        }         
    }
}
