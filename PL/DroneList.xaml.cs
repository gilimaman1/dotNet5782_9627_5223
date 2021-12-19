﻿using System;
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
using IBL;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        IBL.IBL bl;

        Action currFilter;
        public DroneList(IBL.IBL bl)
        {
            this.bl = bl;
            string[] categories = new string[2] {"status", "weight"};
            InitializeComponent();
            ChooseCategory.DataContext = categories;
            StatusFilter.DataContext = typeof(DroneStatuses).GetEnumValues();
            WeightFilter .DataContext = typeof(WeightCategories).GetEnumValues();
            StatusFilter.Visibility = Visibility.Collapsed;
            WeightFilter.Visibility = Visibility.Collapsed;
            StatusFilterLable.Visibility = Visibility.Collapsed;
            WeightFilterLable.Visibility = Visibility.Collapsed;
        }

        private void ChooseCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ChooseCategory.SelectedItem)
            {
                case "status":
                    {
                        WeightFilter.Visibility = Visibility.Collapsed;
                        WeightFilterLable.Visibility = Visibility.Collapsed;
                        StatusFilter.Visibility = Visibility.Visible;
                        StatusFilterLable.Visibility = Visibility.Visible;
                        break;
                    }
                case "weight":
                    {
                        StatusFilterLable.Visibility = Visibility.Collapsed;
                        StatusFilter.Visibility = Visibility.Collapsed;
                        WeightFilter.Visibility = Visibility.Visible;
                        WeightFilterLable.Visibility = Visibility.Visible;
                        break;
                    }
            }
            //{
            //    DroneListView.ItemsSource = (List<DroneForList>)bl.GetDronesForList();
            //}
            //else
            //{
            //    DroneStatuses status = (DroneStatuses)FilterComboBox.SelectedItem;
            //    DroneListView.ItemsSource = (List<DroneForList>)bl.GetFilteredDroneForList(status);
            //}
            
        }

        private void Filter()
        {
            DroneListView.ItemsSource = (List<DroneForList>)bl.GetDronesForList();
        }

        private void FilterStatus()
        {
            Filter();
            DroneStatuses status = (DroneStatuses)StatusFilter.SelectedItem;
            DroneListView.ItemsSource = (List<DroneForList>)bl.GetStatusFilteredDroneForList(status);
            
        }

        private void FilterWeight()
        {
                Filter();
                WeightCategories weight = (WeightCategories)WeightFilter.SelectedItem;
                DroneListView.ItemsSource = (List<DroneForList>)bl.GetWeightFilteredDroneForList(weight);
        }

        private void StatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currFilter = FilterStatus;
            FilterStatus();
        }

        private void WeightFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currFilter = FilterWeight;
            FilterWeight();
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            new SingleDrone(bl, currFilter).Show();
        }

        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new SingleDrone((e.OriginalSource as FrameworkElement).DataContext as IBL.BO.DroneForList, bl, currFilter).Show();
        }
    }
}
