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
    public class ImportCSVCommand : CommandBase
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
        
        public override async void Execute(object? parameter)
        {
            await foreach (var item in FetchItems())
            {
                var values = item.Split(',');
                Card card = new Card(values[0], values[1], values[2], values[3], values[4]);
                await _peopleLibrary.CreateCard(card);
            }

            new LoadCardsCommand(_peopleLibrary, _mainWindowViewModel).Execute(null);
        }

        public async IAsyncEnumerable<string> FetchItems()
        {
            using StreamReader reader = File.OpenText("Users.csv");
            while (!reader.EndOfStream)
                yield return await reader.ReadLineAsync();
        }        
    }

}
