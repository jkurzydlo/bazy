﻿using bazy1.ViewModels.Receptionist;
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

namespace bazy1.Views.Receptionist
{
    /// <summary>
    /// Logika interakcji dla klasy ReceptionistView.xaml
    /// </summary>
    public partial class ReceptionistView : Window
    {
        public ReceptionistView()
        {
            InitializeComponent();
            Console.WriteLine(DataContext.ToString());
        }
    }
}
