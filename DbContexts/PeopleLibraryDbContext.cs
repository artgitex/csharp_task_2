using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task_2.Model;

namespace Task_2.DbContexts
{
    public class PeopleLibraryDbContext : DbContext
    {
        public PeopleLibraryDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=peopledb;Trusted_Connection=True;");
        }
        
        public DbSet<Card> Cards { get; set; }
    }
}
