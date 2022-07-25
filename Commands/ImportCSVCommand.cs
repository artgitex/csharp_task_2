using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task_2.DbContexts;
using Task_2.Model;
using Task_2.ViewModels;

namespace Task_2.Commands
{
    public class ImportCSVCommand : AsyncCommandBase    
    {
        private readonly PeopleLibrary _peopleLibrary;
        private readonly MainWindowViewModel _mainWindowViewModel;        

        public ICommand LoadCards { get; }

        public ImportCSVCommand(PeopleLibrary peopleLibrary, MainWindowViewModel mainWindowViewModel)
        {
            _peopleLibrary = peopleLibrary;
            _mainWindowViewModel = mainWindowViewModel;           
        }

        public override bool CanExecute(object? parameter)
        {
            return _peopleLibrary.IsLibraryEmpty();            
        }

        public override async Task ExecuteAsync(object parameter)
        {           
            var lines = File.ReadAllLines("Users.csv");

            foreach (var line in lines)
            {
                var values = line.Split(',');
                Card card = new Card(values[0], values[1], values[1], values[3], values[4]);
                await _peopleLibrary.CreateCard(card);
            }         
            
            new LoadCardsCommand(_peopleLibrary, _mainWindowViewModel).Execute(null);
        }    
    }
}
