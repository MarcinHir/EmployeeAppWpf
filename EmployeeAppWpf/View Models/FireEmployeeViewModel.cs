using EmployeeAppWpf.Commands;
using EmployeeAppWpf.Models.Domains;
using EmployeeAppWpf.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace EmployeeAppWpf.View_Models
{
    internal class FireEmployeeViewModel : ViewModelBase
    {
        public FireEmployeeViewModel(EmployeeWrapper employee)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            FireEmployeeConfirmCommand = new AsyncRelayCommand(FireEmployeeConfirm);
            Employee = employee;
        }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        public ICommand FireEmployeeConfirmCommand { get; set; }
        private Repository _repository = new Repository();
        private EmployeeWrapper _employee;
        public EmployeeWrapper Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                
            }

        }
        private void Confirm(object obj)
        {
            _ = FireEmployeeConfirm(null);
            CloseWindow(obj as Window);
        }

        private void FireEmployee()
        {

            _repository.FireEmployee(Employee);
        }
        private async Task FireEmployeeConfirm(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Zwalnianie pracownika",
                $"Czy na pewno chcesz zwolnić {Employee.FirstName} " +
                $"{Employee.LastName}",
                MessageDialogStyle.AffirmativeAndNegative);
            
            if (dialog != MessageDialogResult.Affirmative)
                return;

            FireEmployee();
            
        }
        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
