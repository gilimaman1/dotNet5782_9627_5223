﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace IBL.BO
{
    public class Drone
    {
        int id;

        public int Id
        {
            set
            {
                if (value < 0)
                {
                    throw new OverloadException("Id must contain a positive number");
                }
                id = value;
            }
            get { return id; }
        }

        //there's nothing to check for a model - it can hold chars and also digits.
        public string Model { get; set; }

        public WeightCategories MaxWeight { set; get; }

        public double Battery { get; set; }

        public DroneStatuses Status { set; get; }

        public ParcelInPassing Parcel { set; get; }


        public Location MyLocation { get; set; }

        public Drone(int id, string model, WeightCategories maxWeight, double battery, DroneStatuses status, ParcelInPassing parcel, Location myLocation)
        {
            this.id = id; Id = id; Model = model; MaxWeight = maxWeight; Battery = battery; Status = status; Parcel = parcel; MyLocation = myLocation;
        }


        public Drone() { }

        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"model: {Model} \n" +
                   $"maxWeight:  {MaxWeight}\n" +
                   $"battery: {Battery} \n" +
                   $"status: {Status} \n" +
                   $"parcel: {Parcel} \n" +
                   $"myLocation: {MyLocation} \n";
        }
    }
}

