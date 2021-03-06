using System;
using System.Collections.Generic;
using DO;
using System.Text;
using System.Runtime.CompilerServices;

namespace DalApi
{
    public interface IDal
    {
        /// <summary>
        /// the function adds a base station to the BaseStationsList.
        /// </summary>
        /// <param name="baseStation">an object of a base station - for adding to the BaseStationsList</param>
        void Add(BaseStation baseStation);

        /// <summary>
        /// the function adds a drone to the DronesList.
        /// </summary>
        /// <param name="drone">an object of a drone - for adding to the DronesList</param>
        void Add(Drone drone);

        /// <summary>
        /// the function adds a customer to the CustomersList.
        /// </summary>
        /// <param name="customer">an object of a customer - for adding to the CustomersList</param>
        /// 
        void Add(Customer customer);

        /// <summary>
        /// the function adds a parcel to the ParcelsList.
        /// </summary>
        /// <param name="parcel">an object of a parcel - for adding to the ParcelsList</param> 
        void Add(Parcel parcel);

        /// <summary>
        /// the function adds a drone to the DronesChargeList.
        /// </summary>
        /// <param name="droneCharge">an object of a parcel - for adding to the ParcelsList</param> 
        void Add(DroneCharge droneCharge);


        /// <summary>
        /// the function updates drone's model
        /// </summary>
        /// <param name="drone">the updated drone</param>
        /// <param name="id">drone's id</param>
        void UpDate(Drone drone, int id);

        /// <summary>
        /// the function updates a baseStation object in BaseStationsList.
        /// </summary>
        /// <param name="baseStation">the updated base station</param>
        /// <param name="id">base station's id</param>
        void UpDate(BaseStation baseStation, int id);

        /// <summary>
        /// the function updates a customer object in CustomersList.
        /// </summary>
        /// <param name="customer">the updated customer</param>
        /// <param name="id">customer's id</param>
        void UpDate(Customer customer, string id);

        /// <summary>
        /// the function updates a parcel object in ParcelsList.
        /// </summary>
        /// <param name="parcel">the updated parcel</param>
        /// <param name="id">parcel's id</param>
        void UpDate(Parcel parcel, int id);

        /// <summary>
        /// sends the requsted drone to recharge in the requested base station.
        /// </summary>
        /// <param name="droneId">like that, it's possible to identity the requested drone</param>
        /// <param name="baseStationId">like that, it's possible to identity the requested base station</param>
        void SendDroneToRecharge(int droneId, int baseStationId);

        /// <summary>
        /// release the requested drone from the base station it has been charged till now.
        /// </summary>
        /// <param name="droneId">like that, it's possible to identity the requested drone</param>
        BaseStation ReleaseDroneFromRecharge(int droneId);

        /// <summary>
        /// the function removes a DroneCharge object from the DronesChargeList
        /// it also takes care to update the appropriate base station's chargeSlots - 
        /// by increasing them by one.
        /// </summary>
        /// <param name="drone"></param>
        BaseStation Remove(DroneCharge drone);

        /// <summary>
        /// getts a specific base station by its id
        /// </summary>
        /// <param name="requestedId">the base station's id</param>
        /// <returns>the found BaseStation object</returns>
        BaseStation GetBaseStation(int requestedId);

        /// <summary>
        /// getts a specific drone by its id
        /// </summary>
        /// <param name="requestedId">the drone's id</param>
        /// <returns>the found Drone object</returns>
        Drone GetDrone(int requestedId);

        /// <summary>
        /// getts a specific customer by its id
        /// </summary>
        /// <param name="requestedId">the customer's id</param>
        /// <returns>the found Customer object</returns>
        Customer GetCustomer(string requestedId);

        /// <summary>
        /// the function returns a DroneCharge object by the requested id.
        /// </summary>
        /// <param name="droneId">the requested id</param>
        /// <returns>a DroneCharge object</returns>
        DroneCharge GetDroneCharge(int droneId);


        /// <summary>
        /// getts a specific parcel by its id
        /// </summary>
        /// <param name="requestedId">the parcel's id</param>
        /// <returns>the found Parcel object</returns>
        Parcel GetParcel(int requestedId);

        /// <summary>
        /// the functions getts the max amount of childrent,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        /// </summary>
        /// <returns>the baseStationList </returns>
        IEnumerable<BaseStation> GetBaseStationsList();

        /// <summary>
        /// getters the dronesList.
        /// </summary>
        /// <returns>the dronesList</returns>
        IEnumerable<Drone> GetDronesList();

        /// <summary>
        /// getters the customersList
        /// </summary>
        /// <returns>the customersList</returns>
        IEnumerable<Customer> GetCustomersList();

        /// <summary>
        /// getters the parcelsList.
        /// </summary>
        /// <returns>the list - parcelsList.</returns>
        IEnumerable<Parcel> GetParcelsList();

        /// <summary>
        /// builds a base station list containing only the base station which have available chargeSlots
        /// </summary>
        /// <param name="AvailableSlots">a predicate</param>
        /// <returns>the created list is returned</returns>
        IEnumerable<BaseStation> AvailableChargeStations(Predicate<BaseStation> AvailableSlots);

        /// <summary>
        /// computes the amount of the available chargeSlots in the baseStation with the id like the parameter.
        /// </summary>
        /// <param name="baseStationId">the id of the base sattion we want to check.</param>
        /// <returns></returns>
        int CaughtChargeSlots(int baseStationId);

        /// <summary>
        /// creats a list which contains all the drones that are charged in the current baseStation,
        /// </summary>
        /// <param name="stationId"> an id of the baseStation</param>
        /// <returns>a list that contains all the drones {</returns>
        IEnumerable<int> GetDronesIdInBaseStation(int stationId);

        /// <summary>
        /// creates a lists that contains all the drones that are charged in the currStation.
        /// </summary>
        /// <param name="InMe">a predicate</param>
        /// <returns>a list which contauns all the droneCharge objects which are charged in the station that its id is equal to the id we got as a parameter.</returns>
        IEnumerable<DroneCharge> DronesChargingInMe(Predicate<DroneCharge> InMe);

        /// <summary>
        /// creates a list which contains all the parcels with a certain condition (Do not check deletion).
        /// </summary>
        /// <param name="predicate">a predicate</param>
        /// <returns>all the parcels with a certain condition</returns>
        public IEnumerable<Parcel> Parcels(Predicate<Parcel> predicate);
        

        /// <summary>
        /// returns the highest parcel's id
        /// </summary>
        /// <returns>the highest parcel's id</returns>
        int GetLastParcelId();

        /// <summary>
        /// returns station's id where the drone is charged in.
        /// </summary>
        /// <param name="droneId">drone's id</param>
        /// <returns>station's id</returns>
        int GetDroneChargeBaseStationId(int droneId);

        /// <summary>
        ///  returns a double array which contains all the batteryusages
        /// + the charge rate.(5 elements)
        /// </summary>
        /// <returns>double[] type</returns>
        double[] BatteryUsages();
    }
}
