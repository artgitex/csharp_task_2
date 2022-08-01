using Reservoom.Stores;
using System;
using System.Windows.Input;
using Task_2.Commands;
using Task_2.Model;

namespace Task_2.ViewModels;
public class ExportParametersViewModel : ViewModelBase        
{
    private DateTime _loadDate = new DateTime(2021, 11, 26);
    public DateTime LoadDate
    {
        get
        {
            return _loadDate;
        }
        set
        {
            _loadDate = value;
            OnPropertyChanged(nameof(LoadDate));
        }
    }

    private string _firstName;
    public string FirstName
    {
        get
        {
            return _firstName;
        }
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    private string _lastName;
    public string LastName
    {
        get
        {
            return _lastName;
        }
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    private string _city;
    public string City
    {
        get
        {
            return _city;
        }
        set
        {
            _city = value;
            OnPropertyChanged(nameof(City));
        }
    }

    private string _country;
    public string Country
    {
        get
        {
            return _country;
        }
        set
        {
            _country = value;
            OnPropertyChanged(nameof(Country));
        }
    }

    public ICommand XMLCommand { get; }
    public ICommand XLSCommand { get; }
    public ICommand CancelCommand { get; }

    public ExportParametersViewModel(PeopleLibrary peopleLibrary, NavigationStore navigationStore, Func<MainWindowViewModel> createMainWindowViewModel)
    {
        XMLCommand = new SubmitCommand(peopleLibrary, this, "xml");
        XLSCommand = new SubmitCommand(peopleLibrary, this, "xls");
        CancelCommand = new NavigateCommand(navigationStore, createMainWindowViewModel);
    }
}
