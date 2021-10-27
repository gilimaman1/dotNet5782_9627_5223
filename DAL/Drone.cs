﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IDAL
{
    namespace DO
    {
       
        public partial class IDAL
        {
            /// <summary>
            /// the struct Drone contains the following details: id, battery, model, status, maxWeight.
            /// </summary>
            public struct Drone
            {
                int id;
                double battery;
                public int Id
                {
                    set
                    {
                        if (value < 0)
                        {
                            throw new FormatException("Id must contain a positive number");
                        }
                        id = value;
                    }
                    get { return id; }
                }

                //there's nothing to check for a model - it can hold chars and also digits.
                public string Model { get; set; }
                public double Battery
                {
                    get { return battery; }
                    set
                    {
                        if (value < 0)
                            throw new FormatException("Battery must hold a positive value.");
                        if (value > 100)
                            throw new FormatException("Battery can't hold a value more than 100% of charge.");
                        battery = value;
                    }
                }

                public DO.DroneStatuses Status { set; get; }
                public DO.WeightCategories MaxWeight { set; get; }

                /// <summary>
                /// a constructor with parameters
                /// </summary>
                /// <param name="id">modify id</param>
                /// <param name="battery">modify battery</param>
                /// <param name="model">modify model</param>
                /// <param name="status">modify status</param>
                /// <param name="maxWeight">modify maxWeight</param>
                public Drone(int id, double battery, string model, DroneStatuses status, WeightCategories maxWeight)
                {
                    this.id = id; this.battery = battery; Model = model; Status = status; MaxWeight = maxWeight;
                }

                /// <summary>
                /// override ToString function.
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return $"id: {Id} \n" +
                              $"model: {Model} \n" +
                              $"status: {Status}\n" +
                              $"maxWeight:  {MaxWeight}\n" +
                              $"battery: {Battery}\n";
                }
            }
        }
    }
}

