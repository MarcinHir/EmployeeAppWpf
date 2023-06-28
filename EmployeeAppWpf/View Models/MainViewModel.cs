using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EmployeeAppWpf.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using EmployeeAppWpf.Models.Wrappers;
using EmployeeAppWpf.Models.Domains;
using EmployeeAppWpf.Views;
using EmployeeAppWpf.Models;
using System.Windows.Controls;
using System.Diagnostics.Contracts;
using EmployeeAppWpf.Properties;

namespace EmployeeAppWpf.View_Models
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {            
            RefreshEmployeeCommand = new RelayCommand(RefreshEmployee);
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditEmployee);
            FireEmployeeCommand = new RelayCommand(FireEmployee, CanFireEmployee);
            PropertiesCommand = new RelayCommand(Properties);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);
            LoadedWindow(null);
        }

        public ICommand RefreshEmployeeCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand FireEmployeeCommand { get; set; }
        public ICommand PropertiesCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoggingWindowCommand { get; set; }

        private EmployeeWrapper _selectedEmployee;
        private ObservableCollection<EmployeeWrapper> _employee;
        private IsWorking _selectedIsWorking;
        private async void LoadedWindow(object obj)
        {
            if (!IfConnectionToDatabaseIsValid())
            {
                var MetroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await MetroWindow.ShowMessageAsync("Łączenie z bazą danych", "Nie można połączyć z bazą danych. Zmienić ustawienia ?", MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var propertiesWindow = new PropertiesView(false);
                    propertiesWindow.ShowDialog();
                }
            }
            else
            {
                if (Settings.Default.Authorization)
                    RefreshDiary();
                else
                {
                    var loginWindow = new LoginView();
                    loginWindow.ShowDialog();
                    if (Settings.Default.Authorization)
                        RefreshDiary();
                    else
                        Application.Current.Shutdown();
                }
            }           
        }
        public IsWorking SelectedIsWorking
        {
            get { return _selectedIsWorking; }
            set
            {
                _selectedIsWorking = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<IsWorking> SelectedIsWorkingValues
        {
            get
            {
                return Enum.GetValues(typeof(IsWorking)).Cast<IsWorking>();
            }
        }
        int SelectedWorking
        {
            get
            {
                return (int)SelectedIsWorking;
            }
        }
        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<EmployeeWrapper> Employees
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }
        private void RefreshEmployee(object obj)
        {            
            RefreshDiary();
        }
        private bool CanEditEmployee(object obj)
        {
            return SelectedEmployee != null;
        }
        private bool CanFireEmployee(object obj)
        {

            return SelectedEmployee != null;
            
        }
        private void FireEmployee(object obj)
        {
            var fireEmployeeWindow = new FireEmployeeView(obj as EmployeeWrapper);
            fireEmployeeWindow.Closed += AddEditFireEmployee_WindowClosed;
            fireEmployeeWindow.Show();
        }
        private void AddEditEmployee(object obj)
        {
            var addEditStudentWindow = new AddEditEmployeeView(obj as EmployeeWrapper);
            addEditStudentWindow.Closed += AddEditFireEmployee_WindowClosed;
            addEditStudentWindow.Show();
        }

        private void AddEditFireEmployee_WindowClosed(object sender, EventArgs e)
        {
            RefreshDiary();
        }
            
        private void RefreshDiary()
        {             
            Employees = new ObservableCollection<EmployeeWrapper>(_repository.GetEmployee(SelectedWorking));
        }

        private void Properties(object obj)
        {
            var propertiesWindow = new PropertiesView(true);
            propertiesWindow.Closed += PropertiesWindow_Closed;
            propertiesWindow.Show();
        }

        private void PropertiesWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private bool IfConnectionToDatabaseIsValid()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
