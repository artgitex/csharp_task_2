using Reservoom.Stores;
using System;
using Task_2.Model;
using Task_2.ViewModels;

namespace Task_2.Commands;

public class ExportCommand : CommandBase
{
    private readonly PeopleLibrary _peopleLibrary;
    private readonly NavigationStore _navigationStore;
    private readonly Func<ExportParametersViewModel> _createExportParametersViewModel;    

    public ExportCommand(PeopleLibrary peopleLibrary, NavigationStore navigationStore, Func<ExportParametersViewModel> createExportParametersViewModel)
    {
        _peopleLibrary = peopleLibrary;
        _navigationStore = navigationStore;
        _createExportParametersViewModel = createExportParametersViewModel;
    }

    public override void Execute(object? parameter)
    {
        new NavigateCommand(_navigationStore, _createExportParametersViewModel).Execute(null);
    }

    public override bool CanExecute(object? parameter) => true;
}
