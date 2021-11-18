﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IBL.BO
{
    class BaseStationToList
    {
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                {
                    throw new OverloadException("Id must contain a positive number");
                }
                id = value;
            }
        }
        private string name;
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

        private int freeChargeSlots;

        public int FreeChargeSlots
        {
            get
            {
                return freeChargeSlots;
            }
            set
            {
                if (value < 0)
                {
                    throw (new OverloadException("Not valid number of chargeSlots"));
                }

                freeChargeSlots = value;
            }
        }

        private int caughtChargeSlots;

        public int CaughtChargeSlots
        {
            get
            {
                return caughtChargeSlots;
            }
            set
            {
                if (value < 0)
                {
                    throw (new OverloadException("Not valid number of chargeSlots"));
                }

                caughtChargeSlots = value;
            }
        }

        public BaseStationToList(int id, string name,int freeChargeSlots, int caughtChargeSlots)
        {
            this.id = id; this.name = name; this.freeChargeSlots = freeChargeSlots; this.caughtChargeSlots = caughtChargeSlots;
            Id = id; Name = name; FreeChargeSlots = freeChargeSlots; CaughtChargeSlots = freeChargeSlots;
        }
    }
}