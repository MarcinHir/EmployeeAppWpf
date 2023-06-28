using EmployeeAppWpf.Commands;
using EmployeeAppWpf.Models;
using EmployeeAppWpf.Models.Domains;
using EmployeeAppWpf.Models.Wrappers;
using EmployeeAppWpf.Properties;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using EmployeeAppWpf.Views;

namespace EmployeeAppWpf.View_Models
{
    public class LoginViewModel : ViewModelBase
    {
        private UserWrapper _user;
        private Repository _repository = new Repository();
        public UserWrapper User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        public LoginViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            User = new UserWrapper();
        }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }

        private void Close(object obj)
        {                        
                User.Authorization = false;
                User.Save();
                Application.Current.Shutdown();
        }
        private void Confirm(object obj)
        {

            if (!User.IsValid)
            {
                MessageBox.Show("Wprowadź poprawne dane","Błąd logowania", MessageBoxButton.OK);
                return;

            }
            else if (_repository.LogUser(User))
            {
                User.Authorization = true;
                User.Save();
            }
            else
            {
                MessageBox.Show("Wprowadź poprawne dane", "Błąd logowania", MessageBoxButton.OK);
                return;
            }
            CloseWindow(obj as Window);
                
        }
        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
