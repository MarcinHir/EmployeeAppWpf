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

namespace EmployeeAppWpf.View_Models
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            LoadedWindow(null);
            RefreshEmployeeCommand = new RelayCommand(RefreshEmployee);
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditDeleteEmployee);
            FireEmployeeCommand = new AsyncRelayCommand(FireEmployee, CanEditDeleteEmployee);
            PropertiesCommand = new RelayCommand(Properties);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);



        }

        public ICommand RefreshEmployeeCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand FireEmployeeCommand { get; set; }
        public ICommand PropertiesCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        private EmployeeWrapper _selectedEmployee;
        private ObservableCollection<EmployeeWrapper> _employee;
        private int _selectedGroupId;
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
                RefreshDiary();
                //InitGroups();
            }
        }
        //public int SelectedGroupId
        //{
        //    get { return _selectedGroupId; }
        //    set
        //    {
        //        _selectedGroupId = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public ObservableCollection<Group> Groups
        //{
        //    get { return _groups; }
        //    set
        //    {
        //        _groups = value;
        //        OnPropertyChanged();
        //    }
        //}


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
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }


        private void RefreshEmployee(object obj)
        {
            RefreshDiary();
        }

        private bool CanRefreshEmployee(object obj)
        {
            return true;
        }
        //private void InitGroups()
        //{
        //    var groups = _repository.GetGroups();
        //    groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

        //    Groups = new ObservableCollection<Group>(groups);

        //    SelectedGroupId = 0;
        //}
        private bool CanEditDeleteEmployee(object obj)
        {
            return SelectedEmployee != null;
        }

        private async Task FireEmployee(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Zwalnianie pracownika",
                $"Czy na pewno chcesz zwolnić {SelectedEmployee.FirstName} " +
                $"{SelectedEmployee.LastName}",
                MessageDialogStyle.AffirmativeAndNegative);
            ;
            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.FireEmployee(SelectedEmployee.Id);

            RefreshDiary();


        }

        private void AddEditEmployee(object obj)
        {
            var addEditStudentWindow = new AddEditEmployeeView(obj as EmployeeWrapper);
            addEditStudentWindow.Closed += AddEditEmployee_WindowClosed;
            addEditStudentWindow.Show();
        }

        private void AddEditEmployee_WindowClosed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            Employees = new ObservableCollection<EmployeeWrapper>(_repository.GetEmployee(SelectedGroupId));
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
