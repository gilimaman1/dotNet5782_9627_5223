﻿using BO;
using Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using static PL.PO.POConverter;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    sealed partial class ListsModel : Singleton<ListsModel>, INotifyPropertyChanged
    {
        // StationViewModel Lists
 
        BLApi.IBL bl;
        ObservableCollection<BaseStationForList> stations;

        /// <summary>
        /// private constructor
        /// </summary>
        private ListsModel()
        {
            bl = BLApi.BLFactory.GetBl();
            Stations = new ObservableCollection<BaseStationForList>(bl.GetBaseStationList().ToList());
            Customers = new ObservableCollection<PL.PO.CustomerForList>(ListOFCustomerForListBOToPO(bl.GetCustomersList().ToList()));
            Parcels = new ObservableCollection<ParcelForList>(bl.GetParcelsList().ToList());
            Drones = new ObservableCollection<PL.PO.DroneForList>(DroneListBOToPO(bl.GetDronesForList()).ToList());
        }

        public ObservableCollection<BO.BaseStationForList> Stations
        {
            get => stations;
            private set
            {
                stations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stations)));
            }
        }

        /// <summary>
        /// update station
        /// </summary>
        /// <param name="stationId">station's id</param>
        public void UpdateStation(int stationId)
        {
            DeleteStation(stationId);
            AddStation(stationId);
        }

        /// <summary>
        /// delete station
        /// </summary>
        /// <param name="stationId">station's id</param>
        public void DeleteStation(int stationId)
        {
            var updatedStation = Stations.FirstOrDefault(s => s.Id == stationId);
            Stations.Remove(updatedStation);
        }

        /// <summary>
        /// add station
        /// </summary>
        /// <param name="stationId">station's id</param>
        public void AddStation(int stationId)
        {
            Stations.Add(bl.GetBaseStationForList(stationId));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
