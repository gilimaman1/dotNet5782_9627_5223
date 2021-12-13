﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;



namespace IBL
{
    /// <summary>
    /// The class BL is the business logic level 
    /// which has the responsibility of pull & calaulating lists, object etc. from the DAL logic level
    /// the pulling of data from the DAL logic level is done by an IDal object - a field in the BL class.
    /// </summary>
    public partial class BL : IBL
    {
        //the single object which has the responsibility of pulling the data from the DAl logic level.
        internal IDal.IDal dal;
        readonly List<DroneForList> dronesForList;
        readonly double electricityConsumingOfAvailable;
        readonly double electricityConsumingOfLightWeight;
        readonly double electricityConsumingOfHeavyWeight;
        readonly double electricityConsumingOfAverageWeight;
        readonly double chargeRate;

        public BL()
        {
            dal = new DalObject.DalObject();
            dronesForList = (List<DroneForList>)InitDroneForList();
            double[] droneElectricityInfo = dal.ElectricityConsuming();
            electricityConsumingOfAvailable = droneElectricityInfo[0];
            electricityConsumingOfLightWeight = droneElectricityInfo[1];
            electricityConsumingOfAverageWeight = droneElectricityInfo[2];
            electricityConsumingOfHeavyWeight = droneElectricityInfo[3];
            chargeRate = droneElectricityInfo[4];
            Random rand = new();

            for (int i = 0; i < dronesForList.Count; i++)
            {

                BO.Parcel parcelOfDrone = GetBLParcel(dronesForList[i].ParcelId);
                //the parcel hasn't been supplied.
                if (parcelOfDrone.SupplyDate == null &&
                    dronesForList[i].Status == BO.DroneStatuses.Shipment)
                {

                    //the parcel has been accosiated and hasn't been picked up.
                    if (parcelOfDrone.AssociationDate == null &&
                        parcelOfDrone.PickUpDate != null)
                    {
                        dronesForList[i].Location = NearestBaseStation(dronesForList[i], (List<BO.BaseStation>)GetBOBaseStationsList()).Location;
                    }

                    //the parcel has been picked hasn't been supplied - (in the general condition)
                    else
                    {
                        dronesForList[i].Location = GetBOCustomersList().First(item => GetBLParcel(dronesForList[i].ParcelId).Sender.Id == item.Id).Location;
                    }

                    double minBattery = ComputeMinBatteryNeeded(dronesForList[i], GetBLCustomer(GetBLParcel(dronesForList[i].ParcelId).Target.Id));
                    if (minBattery != -1)
                    {
                        dronesForList[i].Battery = RandomBattery(minBattery);
                    }
                }
                else
                {
                    dronesForList[i].Status = (BO.DroneStatuses)rand.Next(1, 3);
                    List<BO.BaseStation> baseStationList = (List<BO.BaseStation>)GetBOBaseStationsList();
                    List<BO.Customer> customersList = (List<BO.Customer>)GetBOCustomersList();
                    switch (dronesForList[i].Status)
                    {
                        case BO.DroneStatuses.Available:
                            {
                                List<CustomerForList> customerForLists = (List<CustomerForList>)CustomersWithSuppliedParcels();
                                if (customerForLists.Count != 0)
                                {
                                    int index = rand.Next(0, customerForLists.Count - 1);
                                    BO.Customer chosenCustomer = customersList.First(item => item.Id == customerForLists[index].Id);
                                    dronesForList[i].Location = chosenCustomer.Location;
                                    double minBattery = ComputeMinBatteryNeeded(dronesForList[i], chosenCustomer);
                                    dronesForList[i].Battery = RandomBattery(minBattery);
                                }
                                break;
                            }

                        case BO.DroneStatuses.Maintenance:
                            {
                                dronesForList[i].Location = baseStationList[rand.Next(0, baseStationList.Count - 1)].Location;
                                dronesForList[i].Battery = rand.Next(0, 20);
                                break;
                            }
                    }
                }
            }
        }

    }
}