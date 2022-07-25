using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.DbContexts;
using Task_2.Model;

namespace Task_2.Services
{
    public class CardRepository : ICardRepository
    {
        public async Task CreateCard(Card card)
        {
            using (PeopleLibraryDbContext db = new PeopleLibraryDbContext())
            {
                db.Cards.Add(card);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Card>> GetAllCards()
        {
            using (PeopleLibraryDbContext db = new PeopleLibraryDbContext())
            {
                IEnumerable<Card> cards = await db.Cards.ToListAsync();

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
}
