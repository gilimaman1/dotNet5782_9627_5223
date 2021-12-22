﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL;

namespace BLApi
{
    public static class BLFactory
    {
        public static IBL GetBl() => BL.Instance;
    }
}
