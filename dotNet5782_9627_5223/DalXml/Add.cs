﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DO;
using System.Xml.Linq;

namespace DalXml
{
    /// <summary>
    ///the class DalObject contains all the needed methods 
    ///which are connected to the data (in DataSource class) of the program.
    /// </summary>
    sealed partial class DalXml
    {
        public void Add(BaseStation baseStation)
        {
            List<BaseStation> baseStations = Dal.XMLTools.LoadListFromXmlSerializer<DO.BaseStation>(baseStationsPath);
            CheckNotExistenceOfBaseStation(baseStation.Id);
            baseStations.Add(baseStation);
            Dal.XMLTools.SaveListToXmlSerializer<BaseStation>(baseStations, baseStationsPath);
        }


        public void Add(Customer customer)
        {
            CheckNotExistenceOfCustomer(customer.Id);
            XElement id = new XElement("id", customer.Id);
            XElement name = new XElement("name", customer.Name);
            XElement phone = new XElement("phone", customer.Phone);
            XElement longitude = new XElement("longitude", customer.Longitude.ToString());
            XElement latitude = new XElement("longitude", customer.Latitude.ToString());
            XElement location = new XElement("location",longitude, latitude);
            XElement myCustomer = new XElement("Customer", id, name, phone, location);
            Dal.XMLTools.CustomersRoot.Add(myCustomer);
            Dal.XMLTools.CustomersRoot.Save(customersPath);
        }


        public void Add(Drone drone)
        {
            List<Drone> drones = Dal.XMLTools.LoadListFromXmlSerializer<DO.Drone>(dronesPath);
            CheckNotExistenceOfDrone(drone.Id);
            drones.Add(drone);
            Dal.XMLTools.SaveListToXmlSerializer<Drone>(drones, baseStationsPath);
        }

        public void Add(Parcel parcel)
        {
            List<Parcel> parcels = Dal.XMLTools.LoadListFromXmlSerializer < DO.Parcel>(parcelsPath);
            CheckNotExistenceOfParcel(parcel.Id);
            parcels.Add(parcel);
            Dal.XMLTools.SaveListToXmlSerializer<Parcel>(parcels, baseStationsPath);
        }

        public void Add(DroneCharge droneCharge)
        {
            List<DroneCharge> dronesCharge = Dal.XMLTools.LoadListFromXmlSerializer<DO.DroneCharge>(droneChargesPath);
            CheckNotExistenceOfDroneCharge(droneCharge.DroneId);
            dronesCharge.Add(droneCharge);
            Dal.XMLTools.SaveListToXmlSerializer<DroneCharge>(dronesCharge, baseStationsPath);
        }
    }
}



