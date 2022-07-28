using Reservoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Task_2.Commands;
using Task_2.Model;

namespace Task_2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly PeopleLibrary _peopleLibrary;
    private readonly ObservableCollection<CardViewModel> _cards;
    
    public IEnumerable<CardViewModel> Cards => _cards;
    public ICommand ImportCSV { get; }
    public ICommand ShowData { get; }
    public ICommand LoadCards { get; }
    public ICommand Export { get; }

    public MainWindowViewModel(PeopleLibrary peopleLibrary, NavigationStore navigationStore, Func<ExportParametersViewModel> createExportParametersViewModel)
    {
        _peopleLibrary = peopleLibrary;
        _cards = new ObservableCollection<CardViewModel>();

        ImportCSV = new ImportCSVCommand(peopleLibrary, this);
        LoadCards = new LoadCardsCommand(_peopleLibrary, this);
        Export = new NavigateCommand(navigationStore, createExportParametersViewModel);                  
    }
            
    public static MainWindowViewModel LoadViewModel(PeopleLibrary peopleLibrary, NavigationStore navigationStore, Func<ExportParametersViewModel> createExportParametersViewModel)
    {
        MainWindowViewModel viewModel = new MainWindowViewModel(peopleLibrary, navigationStore, createExportParametersViewModel);
        viewModel.LoadCards.Execute(null);
        
        return viewModel;
    }        
    
    public void UpdateCards(IEnumerable<Card> cards)
    {
        _cards.Clear();

        foreach (Card card in cards)
        {
            CardViewModel cardViewModel = new CardViewModel(card);
            _cards.Add(cardViewModel);
        }            
    }        
}
