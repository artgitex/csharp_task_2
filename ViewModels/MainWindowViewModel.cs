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
    private readonly ObservableCollection<Card> _cards;
    
    public IEnumerable<Card> Cards => _cards;

    private bool _isLoading;
    public bool IsLoading
    {
        get
        {
            return _isLoading;
        }
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    private int _pageSize = 100;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value;
            OnPropertyChanged(nameof(PageSize));
        }
    }

    public ICommand ImportCSV { get; }    
    public ICommand LoadCards { get; }
    public ICommand Export { get; }
    public ICommand Refresh { get; }

    public MainWindowViewModel(PeopleLibrary peopleLibrary, NavigationStore navigationStore, Func<ExportParametersViewModel> createExportParametersViewModel)
    {
        _peopleLibrary = peopleLibrary;        
        _cards = new ObservableCollection<Card>();

        ImportCSV = new ImportCSVCommand(_peopleLibrary, this);
        LoadCards = new LoadCardsCommand(_peopleLibrary, this);
        Refresh = new RefrseshCardsCommand(_peopleLibrary, this);
        Export = new ExportCommand(_peopleLibrary, navigationStore, createExportParametersViewModel);
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
            _cards.Add(card);
        }            
    }        
}
