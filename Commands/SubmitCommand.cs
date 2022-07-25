using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task_2.ViewModels;

namespace Task_2.Commands
{
    public class SubmitCommand : CommandBase
    {
        private readonly ExportParametersViewModel _exportParametersViewModel;
        public SubmitCommand(ExportParametersViewModel exportParametersViewModel)
        {
            _exportParametersViewModel = exportParametersViewModel;
            _exportParametersViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        
        public override void Execute(object? parameter)
        {
            MessageBox.Show("Data successfully exported", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_exportParametersViewModel.FirstName) &&
                !string.IsNullOrEmpty(_exportParametersViewModel.LastName) &&
                !string.IsNullOrEmpty(_exportParametersViewModel.City) &&
                !string.IsNullOrEmpty(_exportParametersViewModel.Country) &&
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
