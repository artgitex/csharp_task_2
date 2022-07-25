using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.Model;

namespace Task_2.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private readonly Card _card;
        
        public string LoadDate => _card.LoadDate;
        public string FirstName => _card.FirstName;
        public string LastName => _card.LastName;
        public string City => _card.City;
        public string Country => _card.Country;

        public CardViewModel(Card card)
        {
            _card = card;
        }
    }
}
