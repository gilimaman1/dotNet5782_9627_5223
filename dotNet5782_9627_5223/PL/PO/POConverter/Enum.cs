﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.PO
{
    public static partial class POConverter
    {
        //-----------------------------Enums----------------------------
        public enum Directions
        {
            NORTH, EAST, WEST, SOUTH
        }

        //---------------------------Parcel Enums-------------------------
        public enum GroupOptions
        {
            GroupBySender = 1, GroupByTarget
        }

        public enum SortOptions
        {
            SortedId = 1, SortedStatus, SortedWeight, SortedPriority
        }

        public enum Priorities
        {
            Standard = 1, Fast, Emergency
        }

        /// <summary>
        /// an enum which contains the four statuses of passing a parcel can be in.
        /// </summary>
        public enum ParcelStatuses
        {
            Production, Associated, PickedUp, Supplied
        }

        /// <summary>
        /// an enum which contains all the three categories of weight a parcel can weigh.
        /// </summary>
        public enum WeightCategories
        {
            None = 0, Light = 1, Average, Heavy
        }


        /// <summary>
        /// an enum which contains the three actions of a parcel (in update option) which have an appropriate Exception.
        /// </summary>
        public enum ParcelActions
        {
            Associate, PickUp, Supply, SendforRecharge,
        }


        /// <summary>
        /// an enum which signs if the parcel was sended from the customer or was sended to him.
        /// </summary>
        public enum FromOrTo
        {
            From, To
        }


        //-------------------------Customer Enums-----------------------
        /// <summary>
        /// 
        /// an enum which contains the two options for a location - longitude / latitude.
        /// </summary>
        public enum Locations
        {
            Latitude, Longitude
        }


        //----------------------------Drone Enum------------------------
        /// <summary>
        /// an enum which contains the three statuses a drone can be in.
        /// </summary>
        public enum DroneStatuses
        {
            None = 0, Available = 1, Maintenance, Shipment
        }

    }
}