using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;


namespace IBL
{
    public partial class BL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int droneId, string model)
        {
            lock (dal)
            {
                DO.Drone drone = dal.GetDrone(droneId);
                drone.Model = model;
                dal.UpDate(drone, droneId);
                DroneForList droneForList = dronesForList.First(drone => drone.Id == droneId);
                droneForList.Model = model;
                UpdateDrone(droneForList);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateBaseStation(int baseStationId, string name, string chargeSlots)
        {
            lock (dal)
            {
                DO.BaseStation station = dal.GetBaseStation(baseStationId);
                if (name != null) { station.Name = name; }
                if (chargeSlots != null)
                {
                    int chargeSlots1 = int.Parse(chargeSlots);
                    //the amount of drones thet charged in this baseStation
                    //is bigger than its available chargeSLots.
                    if (dal.CaughtChargeSlots(baseStationId) > chargeSlots1)
                        throw new BLChargeSlotsException(chargeSlots1);
                    station.ChargeSlots = chargeSlots1;
                }
                dal.UpDate(station, baseStationId);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(string customerId, string name, string phone)
        {
            lock (dal)
            {
                DO.Customer customer = dal.GetCustomer(customerId);
                if (name != null) { customer.Name = name; }
                if (phone != null) { customer.Phone = phone; }
                dal.UpDate(customer, customerId);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(DroneForList droneForList)
        {
            lock (dal)
            {
                UpDateDroneForList(droneForList);
                DO.Drone drone = ConvertBoToDoDrone(ConvertDroneForListToDrone(droneForList));
                dal.UpDate(drone, drone.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpDateBaseStation(BaseStation baseStation)
        {
            lock (dal)
            {
                DO.BaseStation baseStation1 = dal.GetBaseStation(baseStation.Id);
                dal.UpDate(baseStation1, baseStation1.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpDateDroneForList(DroneForList droneForList)
        {
            lock(dal)
            {
                if (dronesForList.FindIndex(item => item.Id == droneForList.Id) == -1)
                {
                    throw new BLIntIdException(droneForList.Id);
                }
                dronesForList.Remove(dronesForList.First(item => item.Id == droneForList.Id));
                dronesForList.Add(droneForList);
            }
           
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            lock (dal)
            {
                DO.Parcel parcel1 = ConvertBoToDoParcel(parcel);
                dal.UpDate(parcel1, parcel1.Id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel Associateparcel(DroneForList drone)
        {
            lock (dal)
            {
                if (drone.Status != DroneStatuses.Available)
                    throw new DroneStatusException(drone.Status);
                Parcel p = (from parcel in GetBOParcelsList()
                            where parcel.SupplyDate == null && parcel.Weight <= drone.MaxWeight && RequiredBattery(drone, parcel.Id) <= drone.Battery
                            orderby parcel.Priority descending, parcel.Weight descending
                            select parcel).FirstOrDefault();
                if (p == null)
                    throw new ParcelActionsException(ParcelActions.Associate);
                p.MyDrone = new DroneInParcel { Id = drone.Id, Battery = drone.Battery, CurrentLocation = drone.Location };
                p.AssociationDate = DateTime.Now;
                drone.Status = DroneStatuses.Shipment;
                dal.UpDate(ConvertBoToDoParcel(p), p.Id);               
                drone.ParcelId = p.Id;
                dronesForList.Add(drone);
                Drone drone2 = GetBLDrone(drone.Id);
                drone2.Parcel = new ParcelInPassing { Id = p.Id, Priority = p.Priority, Sender = p.Sender, Target = p.Target, Weight = p.Weight };
                UpdateDrone(drone);
                dal.UpDate(ConvertBoToDoDrone(drone2), drone2.Id);
                return p;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private double RequiredBattery(ILocatable drone,int parcelId)
        {
            Parcel parcel = GetBLParcel(parcelId);
            Customer sender = GetBLCustomer(parcel.Sender.Id);
            Customer target = GetBLCustomer(parcel.Target.Id);
            double battery = 0;
            if (target != null)
            {
                battery = BatteryUsages[(int)Enum.Parse(typeof(BatteryUsage), parcel.Weight.ToString())] * sender.Distance(target);
            }
            else
            {

            }
            BaseStation item = NearestBaseStation(target,GetBOBaseStationsList());
            if (item != null)
            {
                battery += BatteryUsages[DRONE_FREE] * target.Distance(item);
            }
            else { }
            if( sender != null)
            {
                if (parcel.SupplyDate is null)
                    battery += BatteryUsages[DRONE_FREE] * drone.Distance(sender);
            }
            else { }
            return battery;
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void AssociateParcel(int droneId)
        //{
        //    lock (dal)
        //    {
        //        DroneForList currentDrone = GetDroneForList(droneId);
        //        List<Customer> customersList = (List<Customer>)GetBOCustomersList();
        //        bool isAssociate = false;
        //        if (currentDrone.Status == DroneStatuses.Available)
        //        {
        //            var parcels = dal.GetParcelsList()
        //                 .OrderByDescending(parcel => (int)parcel.Priority)
        //                 .ThenByDescending(parcel => (int)parcel.Weight)
        //                 .ThenBy(parcel => customersList.First(customer => customer.Id == parcel.SenderId).Distance(currentDrone));
        //            List<BaseStationForList> availableBaseStations = (List<BaseStationForList>)GetAvailableChargeSlots();
        //            foreach (var item in parcels)
        //            {
        //                Parcel parcel = GetBLParcel(item.Id);
        //                if (DroneReachLastDestination(currentDrone, parcel))
        //                {
        //                    if (availableBaseStations != null)
        //                    {
        //                        Customer target = GetBLCustomer(parcel.Target.Id);
        //                        List<BaseStation> baseStations1 = (List<BaseStation>)ConvertBaseStationsForListToBaseStation(availableBaseStations);
        //                        BaseStation nearestBaseStation = NearestBaseStation(currentDrone, baseStations1);
        //                        isAssociate = true;
        //                        currentDrone.Status = DroneStatuses.Shipment;
        //                        currentDrone.ParcelId = parcel.Id;
        //                        if (BatteryRemainedInLastDestination(currentDrone, parcel) <= 0)
        //                        {
        //                            if (nearestBaseStation.ChargeSlots > 0)
        //                            {
        //                                currentDrone.Status = DroneStatuses.Maintenance;
        //                                DO.DroneCharge droneCharge = new() { DroneId = currentDrone.Id, StationId = nearestBaseStation.Id, EntryTime = DateTime.Now };
        //                                dal.Add(droneCharge);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            currentDrone.Battery = BatteryRemainedInLastDestination(currentDrone, parcel);
        //                        }
        //                    }
        //                    currentDrone.Status = DroneStatuses.Shipment;
        //                    UpdateDrone(currentDrone);
        //                    parcel.AssociationDate = DateTime.Now;
        //                    parcel.MyDrone = GetBLDroneInParcel(droneId);
        //                    UpdateParcel(parcel);
        //                    ParcelForList parcel1 = GetParcelForList(parcel.Id);
        //                    parcel1.Status = ParcelStatuses.Associated;
        //                    break;
        //                }
        //            }
        //            if (isAssociate == false)
        //                throw new ParcelActionsException(ParcelActions.Associate);
        //        }
        //        else
        //            throw new DroneStatusException(currentDrone.Status);
        //    }
        //}

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public bool AssociateParcel(DroneForList drone, Parcel parcel)
        //{
        //    return DroneReachLastDestination(drone, parcel);
        //}

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickUpParcel(int droneId)
        {
            bool isPickedUp;
            DroneForList currDrone = GetDroneForList(droneId);  
            //Customer sender = GetBLCustomer(parcelForList.SenderId);
            //the drone is in shipment status' but the parcel still wasn't picked up.
            if (currDrone.Status == DroneStatuses.Shipment)
            {
                ParcelForList parcelForList = GetParcelForList(currDrone.ParcelId);
                if (parcelForList.Status == ParcelStatuses.Associated)
                {
                    isPickedUp = true;
                    //currDrone.Battery = ComputeBatteryRemained(currDrone, sender);
                    //currDrone.Location = sender.Location;
                    Parcel parcel = ConvertParcelForListToParcel(parcelForList);
                    parcel.PickUpDate = DateTime.Now;
                    parcel.MyDrone = new(droneId, currDrone.Battery, currDrone.Location);
                    UpdateParcel(parcel);
                }
                else
                    throw new ParcelStatusException(parcelForList.Status);
            }
            else
                throw new DroneStatusException(currDrone.Status);
            if (isPickedUp == false)
                throw new ParcelActionsException(ParcelActions.PickUp);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SupplyParcel(int droneId)
        {
            bool isSupplied;
            DroneForList drone = GetDroneForList(droneId);
            Parcel parcel1 = GetBLParcel(drone.ParcelId);
            Customer target = GetBLCustomer(parcel1.Target.Id);
            if (parcel1.PickUpDate != null)
            {
                if (parcel1.SupplyDate == null)
                {
                    ParcelForList parcelForList = GetParcelForList(drone.ParcelId);
                    isSupplied = true;
                    drone.Battery = ComputeMinBatteryNeeded(drone, target);
                    drone.Location = target.Location;
                    drone.Status = DroneStatuses.Available;
                    parcel1.SupplyDate = DateTime.Now;
                    drone.ParcelId = 0;
                    parcel1.MyDrone = new();
                    dronesForList.Remove(dronesForList.FirstOrDefault(item=>item.Id == drone.Id));
                    dronesForList.Add(drone);
                    UpdateParcel(parcel1);
                    UpdateDrone(drone);
                }
                else
                    throw new ParcelStatusException(ParcelStatuses.Associated);
            }
            else
                throw new ParcelStatusException(ParcelStatuses.PickedUp);
            if (isSupplied == false)
                throw new ParcelActionsException(ParcelActions.Supply);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation SendDroneForCharge(int droneId)
        {
            DroneForList drone = GetDroneForList(droneId);
            if (drone.Status == DroneStatuses.Available)
            {
                List<BaseStationForList> availableBaseStations = (List<BaseStationForList>)GetAvailableChargeSlots();
                if (availableBaseStations != null)
                {
                    lock (dal)
                    {
                        List<BaseStation> baseStations1 = (List<BaseStation>)ConvertBaseStationsForListToBaseStation(availableBaseStations);
                        BaseStation baseStation = NearestBaseStation(drone, baseStations1);
                        double batteryNeeded = BatteryUsages[DRONE_FREE] * drone.Distance(baseStation);
                        if (baseStation.ChargeSlots == 0)
                            throw new ParcelActionsException(ParcelActions.SendforRecharge);
                        drone.Battery -= batteryNeeded;
                        if (drone.Battery < 0)
                            throw new ParcelActionsException(ParcelActions.SendforRecharge);
                        drone.Location = baseStation.Location;
                        drone.Status = DroneStatuses.Maintenance;
                        DroneInCharging drone1 = new() { Battery = drone.Battery, Id = droneId };
                        baseStation.DroneCharging.Add(drone1);
                        //the amount of available chargeSlots is decrease by one
                        //while adding the drone to chargeDrone. 
                        UpdateDrone(drone);
                        dal.SendDroneToRecharge(drone.Id, baseStation.Id);
                        dronesForList.Add(drone);
                        UpDateBaseStation(baseStation);
                        return baseStation;
                    }
                }
                else
                    throw new ParcelActionsException(ParcelActions.SendforRecharge);
            }
            else
                throw new DroneStatusException(drone.Status);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation ReleaseDroneFromRecharge(int droneId)
        {
            lock (dal)
            {
                DroneForList drone = GetDroneForList(droneId);
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    TimeSpan timeSpan = DateTime.Now - dal.GetDroneCharge(droneId).EntryTime.GetValueOrDefault();
                    drone.Battery = Min(100, drone.Battery + BatteryUsages[DRONE_CHARGE] * timeSpan.TotalMilliseconds/100);
                    drone.Status = DroneStatuses.Available;
                    //the chargeSlots is increased by one within the function 'Remove'
                    //which treats the case a removing of a droneCharge from DronesChargeList occurs.
                    UpdateDrone(drone);
                    return ConvertBaseStationDOtOBO(dal.ReleaseDroneFromRecharge(drone.Id));

                }
                else
                    throw new DroneStatusException(drone.Status);
            }
        }
        

    }
}


