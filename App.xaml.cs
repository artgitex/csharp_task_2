using Reservoom.Stores;
using System.Windows;
using Task_2.Model;
using Task_2.Services;
using Task_2.ViewModels;

namespace Task_2;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore _navigationStore;
    private readonly PeopleLibrary _peopleLibrary;        

    public App()
    {            
        CardRepository cardRepository = new CardRepository();
     
        _peopleLibrary = new PeopleLibrary("US-EU People Library", cardRepository);
        _navigationStore = new NavigationStore();            
    }

    protected override void OnStartup(StartupEventArgs e)
    {               
        _navigationStore.CurrentViewModel = CreateMainWindowViewModel();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };

        MainWindow.Show();

        base.OnStartup(e);
    }

    private MainWindowViewModel CreateMainWindowViewModel()
    {        
        return MainWindowViewModel.LoadViewModel(_peopleLibrary, _navigationStore, CreateExportParametersViewModel);
    }

    private ExportParametersViewModel CreateExportParametersViewModel()
    {
        return new ExportParametersViewModel(_peopleLibrary, _navigationStore, CreateMainWindowViewModel);
    }
}
