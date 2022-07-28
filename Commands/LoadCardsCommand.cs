﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Task_2.Model;
using Task_2.ViewModels;

namespace Task_2.Commands;

public class LoadCardsCommand : AsyncCommandBase
{
    private readonly PeopleLibrary _peopleLibrary;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public LoadCardsCommand(PeopleLibrary peopleLibrary, MainWindowViewModel mainWindowViewModel)
    {
        _peopleLibrary = peopleLibrary;
        _mainWindowViewModel = mainWindowViewModel;
    }
    
    public override async Task ExecuteAsync(object parameter)
    {
        try
        {
            IEnumerable<Card> cards = await _peopleLibrary.GetAllCards();
            _mainWindowViewModel.UpdateCards(cards);
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
