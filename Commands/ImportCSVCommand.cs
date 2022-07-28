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
using Microsoft.Win32;

namespace Task_2.Commands
{    
    public class ImportCSVCommand : CommandBase
    {
        private readonly PeopleLibrary _peopleLibrary;
        private readonly MainWindowViewModel _mainWindowViewModel;  
        private string fileName;

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
                fileName = openFileDialog.FileName;

            List<Card> cards = new List<Card>();

            await foreach (var item in AsyncFetchItems())
            {                                
                var values = item.Split(';');
                Card card = new Card(values[0], values[1], values[2], values[4], values[5]);

                if (cards.Count <= 1000)
                    cards.Add(card);
                else
                {
                    await _peopleLibrary.CreateCards(cards);
                    cards.Clear();
                    cards.Add(card);
                }                                
            }

            if (cards.Count != 0)
                await _peopleLibrary.CreateCards(cards);

            MessageBox.Show("Loading completed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            //new LoadCardsCommand(_peopleLibrary, _mainWindowViewModel).Execute(null);            
        }

        public async IAsyncEnumerable<string> AsyncFetchItems()
        {
            using StreamReader reader = File.OpenText(fileName);
            
            while (!reader.EndOfStream)                
                yield return await reader.ReadLineAsync();
        }        
    }

}
