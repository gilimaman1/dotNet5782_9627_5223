﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static ConsoleUI_BL.Program;
using static IBL.BO.WeightCategories;
using static IBL.BL;


namespace ConsoleUI_BL
{
    /// <summary>
    /// Navigates the add choice appropriate to the user's choice.
    /// </summary>
    public class AddOption : ISubNavigate
    {
        public void options(ref IBL.BL bl)
        {
            int innerChoice ;
            int id = 0, baseStationId = 0;
            string name = " ", customerId = " ", phone = " ", model = " ", senderId = " ", targetId = " ";
            int chargeSlots = 0;

            Location location = new Location();
            //all the enum type litteral are entered as string type
            //and then checked if they contain an enum name.
            string maxWeight = "", weight = "", priority = "";
            WeightCategories weightCategory = WeightCategories.Average;
            Priorities priorities = Priorities.Emergency;

            Console.WriteLine("Please enter : \n1- For Base Station \n2- For Drone\n3- For Customer\n4- For Parcel ");
            if (int.TryParse(Console.ReadLine(), out innerChoice))
            {
                switch (innerChoice)
                {
                    case (int)AddOptions.BaseStation:
                        {
                            CheckBaseStationDetails(ref id, ref name, ref location, ref chargeSlots);
                            BaseStation baseStation = new BaseStation() { Id = id, Name = name, Location = location, ChargeSlots = chargeSlots,DroneCharging = null };
                            bl.Add(baseStation);
                            break;
                        }

                    case (int)AddOptions.Drone:
                        {
                            CheckDroneDetails(ref id, ref model, maxWeight,ref weightCategory, ref baseStationId);
                            Drone drone = new Drone() { Id = id, Model = model , MaxWeight = weightCategory ,Location = bl.GetBLBaseStation(id).Location, Parcel = null};
                            bl.Add(drone, baseStationId);
                            break;
                        }
                    case (int)AddOptions.Customer:
                        {
                            CheckCustomerDetails(ref customerId, ref name, ref phone, ref location);
                            Customer customer = new Customer() { Id = customerId, Name = name, Phone = phone, Location = location, FromCustomer = null, ToCustomer = null };
                            bl.Add(customer);
                            break;
                        }
                    case (int)AddOptions.Parcel:
                        {
                            CheckParcelDetails(ref senderId, ref targetId,  weight, ref weightCategory,  priority, ref priorities);
                            CustomerInParcel sender = new CustomerInParcel() { Id = senderId, Name = "" };
                            CustomerInParcel target = new CustomerInParcel() { Id = targetId, Name = "" };
                            Parcel parcel = new Parcel() { Id = GetParcelIndex(), Sender = sender, Target = target, Weight = weightCategory , Priority = priorities };
                            bl.Add(parcel);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR! \nan unknown option");
                            break;
                        }
                }
            }
            else Console.WriteLine("The add option must hold a numeric value!");
        }
    }
}
