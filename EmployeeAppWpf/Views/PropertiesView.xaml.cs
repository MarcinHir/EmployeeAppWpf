using EmployeeAppWpf.View_Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeAppWpf.Views
{
    /// <summary>
    /// Interaction logic for PropertiesView.xaml
    /// </summary>
    public partial class PropertiesView : MetroWindow
    {
        public PropertiesView(bool IsPropertiesChangedWhileRunning)
        {
            InitializeComponent();
            DataContext = new PropertiesViewModel(IsPropertiesChangedWhileRunning);
        }
    }
}
