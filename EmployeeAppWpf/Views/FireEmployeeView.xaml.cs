﻿using EmployeeAppWpf.Models.Wrappers;
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
    /// Interaction logic for FireEmployeeView.xaml
    /// </summary>
    public partial class FireEmployeeView : MetroWindow
    {
        public FireEmployeeView(EmployeeWrapper employee)
        {
            InitializeComponent();
            DataContext = new FireEmployeeViewModel(employee);
        }
    }
}
