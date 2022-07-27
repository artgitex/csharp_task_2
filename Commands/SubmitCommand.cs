using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task_2.ViewModels;
using System.Xml.Linq;
using Task_2.Model;
using ClosedXML.Excel;

namespace Task_2.Commands
{    
    public class SubmitCommand : AsyncCommandBase
    {
        private readonly ExportParametersViewModel _exportParametersViewModel;
        private readonly PeopleLibrary _peopleLibrary;
        private readonly string _type;
        string xmlFile = "Files\\PeopleExtract.xml";

        public SubmitCommand(PeopleLibrary peopleLibrary, ExportParametersViewModel exportParametersViewModel, string type)
        {
            _exportParametersViewModel = exportParametersViewModel;
            _peopleLibrary = peopleLibrary;
            _type = type;
            _exportParametersViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Card cardFilter = new Card(
                    _exportParametersViewModel.LoadDate.ToString("d"),
                    _exportParametersViewModel.FirstName,
                    _exportParametersViewModel.LastName,
                    _exportParametersViewModel.City,
                    _exportParametersViewModel.Country
                    );

                IEnumerable<Card> cards = await _peopleLibrary.GetFilteredCards(cardFilter);

                if (_type == "xml")
                {                    
                    XDocument doc = XDocument.Load(xmlFile);

                    foreach (Card card in cards)
                    {
                        doc.Root.Add(new XElement("Record",
                            new XAttribute("Id", card.Id),
                            new XElement("LoadDate", card.LoadDate),
                            new XElement("FirstName", card.FirstName),
                            new XElement("LastName", card.LastName),
                            new XElement("County", card.Country),
                            new XElement("City", card.City)));
                    }

                    doc.Save(xmlFile);
                }
                if (_type == "xls")
                {

                    using var wbook = new XLWorkbook();

                    var ws = wbook.Worksheets.Add("Sheet1");

                    ws.Cell(1, 1).Value = "Id";
                    ws.Cell(1, 2).Value = "LoadDate";
                    ws.Cell(1, 3).Value = "FirstName";
                    ws.Cell(1, 4).Value = "LastName";
                    ws.Cell(1, 5).Value = "County";
                    ws.Cell(1, 6).Value = "City";

                    int x = 2;
                    int y = 1;

                    foreach (Card card in cards)
                    {
                        ws.Cell(x, y).Value = card.Id;
                        ws.Cell(x, y + 1).Value = card.LoadDate;
                        ws.Cell(x, y + 2).Value = card.FirstName;
                        ws.Cell(x, y + 3).Value = card.LastName;
                        ws.Cell(x, y + 4).Value = card.City;
                        ws.Cell(x, y + 5).Value = card.Country;

                        x++; y = 1;
                    }                    

                    wbook.SaveAs("PeopleExtract.xlsx");
                }

                MessageBox.Show("Export completed, check bin folder!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
            
            return !string.IsNullOrEmpty(_exportParametersViewModel.FirstName) ||
                !string.IsNullOrEmpty(_exportParametersViewModel.LastName) ||
                !string.IsNullOrEmpty(_exportParametersViewModel.City) ||
                !string.IsNullOrEmpty(_exportParametersViewModel.Country) ||
                base.CanExecute(parameter);            
        }
        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ExportParametersViewModel.FirstName) ||
               e.PropertyName == nameof(ExportParametersViewModel.LastName) ||
               e.PropertyName == nameof(ExportParametersViewModel.City) ||
               e.PropertyName == nameof(ExportParametersViewModel.Country)
               )
            {
                OnCanExecutedChanged();
            }
        }
    }
}
