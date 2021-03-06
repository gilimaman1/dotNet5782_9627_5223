using BO;
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
        #region PrivateFields
        BLApi.IBL bl;
        ObservableCollection<PO.BaseStationForList> stations;
        #endregion

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PO.BaseStationForList> Stations
        {
            get => stations;
            private set
            {
                stations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stations)));
            }
        }

        #endregion

        #region CRUD_Methods

        /// <summary>
        /// update station
        /// </summary>
        /// <param name="stationId">station's id</param>
        public void UpdateStation(int stationId)
        {
            PO.BaseStationForList station = Stations.FirstOrDefault(station => station.Id == stationId);
            int index = Stations.IndexOf(station);
            DeleteStation(station.Id);
            Stations.Insert(index, StationForListBOToPO(bl.GetBaseStationForList(stationId)));
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
            Stations.Add(StationForListBOToPO(bl.GetBaseStationForList(stationId)));
        }

        #endregion

        #region Constructor
        /// <summary>
        /// private constructor
        /// </summary>
        private ListsModel()
        {
            bl = BLApi.BLFactory.GetBl();
            Stations = new ObservableCollection<PO.BaseStationForList>(ListOfStationForListBOToPO(bl.GetBaseStationList()).ToList());
            Customers = new ObservableCollection<PO.CustomerForList>(ListOFCustomerForListBOToPO(bl.GetCustomersList().ToList()));
            Parcels = new ObservableCollection<PO.ParcelForList>(ListOfParcelForListBOToPO(bl.GetParcelsList()).ToList());
            Drones = new ObservableCollection<PO.DroneForList>(DroneListBOToPO(bl.GetDronesForList()).ToList());
        }


        #endregion

    }
}
