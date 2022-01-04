﻿using BO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class StationsListViewModel : INotifyPropertyChanged
    {
        BLApi.IBL bl;
        public StationsListViewModel(BLApi.IBL bl)
        {
            this.bl = bl;
            Cancel = new(Button_ClickCancel, null);
            Add = new(Button_ClickAdd, null);
            Options = new List<string>() { "All BaseStations", "Group By Free ChargeSlots" };
            AllStations = new(bl.GetBaseStationList().ToList());
            Button_AllStations();
            LeftDoubleClick = new(DroneListView_MouseDoubleClick, null);
        }
        public RelayCommand Cancel { get; set; }
        public RelayCommand Add { get; set; }
        public RelayCommand LeftDoubleClick { get; set; }
        public List<string> Options { get; set; }
        private ListCollectionView allStations;
        public ListCollectionView AllStations
        {
            get => allStations;
            set
            {
                allStations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllStations)));
            }
        }
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("AvailableChargeSlots");
        SortDescription sortFree = new("AvailableChargeSlots", 0);
        SortDescription sortId = new("Id", 0);

        private string selectedFilter;
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                selectedFilter = value;
                if (value == Options[0]) Button_AllStations(); else Button_GroupByChargeSlots();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFilter)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //---------------------------------Stations's Methods------------------------------
        /// <summary>
        /// the function treats the event of clicking on the button 'Cancel'.
        /// </summary>
        /// <param name="sender">the invoking object</param>
        /// <param name="e">the event</param>
        private void Button_ClickCancel(object sender)
        {
            (sender as Window).Close();
        }

        /// <summary>
        /// the function treats the event of clicking on the button 'Add'.
        /// </summary>
        private void Button_ClickAdd(object sender)
        {
            new StationView(new StationViewModel(bl)).Show();
        }
        private void DroneListView_MouseDoubleClick(object sender)
        {
            new StationView(new StationViewModel(bl, sender as BaseStationForList)).Show();
        }


        private void Button_AllStations()
        {
            AllStations.GroupDescriptions.Remove(groupDescription);
            AllStations.SortDescriptions.Remove(sortFree);
            AllStations.SortDescriptions.Add(sortId);
        }

        private void Button_GroupByChargeSlots()
        {
            AllStations.GroupDescriptions.Add(groupDescription);
            AllStations.SortDescriptions.Remove(sortId);
            AllStations.SortDescriptions.Add(sortFree);
        }
    }
}
