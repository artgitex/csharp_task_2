using Reservoom.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task_2.Commands;

namespace Task_2.ViewModels
{
    public class ExportParametersViewModel : ViewModelBase        
    {
        private DateTime _loadDate = new DateTime(2022, 1, 1);
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

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public ExportParametersViewModel(NavigationStore navigationStore, Func<MainWindowViewModel> createMainWindowViewModel)
        {
            SubmitCommand = new SubmitCommand(this);
            CancelCommand = new NavigateCommand(navigationStore, createMainWindowViewModel);
        }
    }
}
