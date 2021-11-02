﻿using System;
using System.Collections.Generic;
using System.Text;
using static DalObject.DalObject;
using static IBL.BO.Locations;
using IBL.BO;
using IDAL.DO;
using static IDAL.DO.OverloadException;

namespace IBL
{
    namespace BO
    {
        
            /// <summary>
            /// the struct Customer contains the following details: id, name, phone, longitude, latitude.
            /// </summary>
            public class Customer
            {
                private string id;
                private string name;
                private string phone;
                private double longitude;
                private double latitude;
                public string Id
                {
                    get
                    {
                        return id;
                    }
                    set
                    {
                        if (value.Length != 9)
                        {
                            throw new OverloadException("Id must include exactly 9 digits");
                        }
                        foreach (char digit in value)
                        {
                            if (!Char.IsDigit(digit))
                            {
                                throw new OverloadException("Id must include only digits");

                            }
                        }
                        id = value;
                    }
                }
                public string Name
                {
                    get
                    {
                        return name;
                    }
                    set
                    {
                        foreach (char letter in value)
                        {
                            if (letter != ' ')
                            {
                                if (!Char.IsLetter(letter))
                                {
                                    throw new OverloadException("Name can contain only letters.");
                                }
                            }
                        }
                        name = value;
                    }
                }
                public string Phone
                {
                    get
                    {
                        return phone;
                    }
                    set
                    {
                        if (value[0] != '0')
                            throw new OverloadException("The first digit of a phone number must be '0'");
                        foreach (char digit in value)
                        {
                            if (!Char.IsDigit(digit))
                            {
                                throw new OverloadException("Phone must include only digits");

                            }
                        }
                        phone = value;
                    }
                }
                public double Longitude
                {
                    get
                    {
                        return longitude;
                    }
                    set
                    {
                        if (value < -180 || value > 180)
                        {
                            throw new OverloadException("Longitude must be a positive number and in range of - 180º to 180º.");
                        }

                        longitude = value;
                    }
                }
                public double Latitude
                {
                    get
                    {
                        return latitude;
                    }
                    set
                    {
                        if (value < -180 || value > 180)
                        {
                            throw new OverloadException("Latitude must be a positive number and in range of - 180º to 180º.");
                        }
                        latitude = value;
                    }
                }

                /// <summary>
                /// a constructor with parameters
                /// </summary>
                /// <param name="id">modify id</param>
                /// <param name="name">modify name</param>
                /// <param name="phone">modify phone</param>
                /// <param name="longitude">modify longitude</param>
                /// <param name="latitude">modify latitude</param>
                public Customer(string id, string name, string phone, double longitude, double latitude)
                {
                    this.id = id; this.name = name; this.phone = phone; this.longitude = longitude; this.latitude = latitude;
                    Id = id; Name = name; Phone = phone; Longitude = longitude; Latitude = latitude;
                }

            public Customer() { }
                /// <summary>
                /// override ToString function.
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    return $"id: {Id} \n" +
                              $"name: {Name} \n" +
                              $"phone: {Phone}\n" +
                              $"longitude: {DataSource.coordinate.CastDoubleToCoordinante(Longitude, LONGITUDE)}\n" +
                              $"latitude:  {DataSource.coordinate.CastDoubleToCoordinante(Latitude, LATITUDE)}\n";
                }
            }
        
    }
}