﻿using IBL.BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        public Drone(IBL.IBL bl)
        {
            InitializeComponent();
            this.DataContext = "true";
            status.DataContext = typeof(WeightCategories).GetEnumValues();
            weight.DataContext = typeof(DroneStatuses).GetEnumValues();
        }

        

    }
}
