﻿using bazy1.ViewModels.Admin.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bazy1.Views.Admin.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    public partial class AdminMedicalHistoryView : System.Windows.Controls.UserControl
    {
        public AdminMedicalHistoryView()
        {
            InitializeComponent();
        }
        public AdminMedicalHistoryView(AdminMedicalHistoryViewModel viewModel)
        {
            InitializeComponent();
        }
    }
}
