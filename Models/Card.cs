using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2.Model
{
    public class Card
    {
        public int Id { get; set; }
        public string LoadDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string City { get; set; }
        public string Country { get; set; }

        public Card(string loadDate, string firstName, string lastName, string city, string country)
        {
            LoadDate = loadDate;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Country = country;
        }
    }
}
