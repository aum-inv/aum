﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.PP.Chakra
{
    public class S_VolumeItemData : A_CLOSE
    {
        public S_VolumeItemData() { }

        public S_VolumeItemData(Single volume, DateTime dt)
        {
            base.ClosePrice1 = 0;
            base.Volume = volume;
            base.DTime = dt;
        }

        public S_VolumeItemData(string itemCode, Single volume, DateTime dt)
        {
            base.ItemCode = itemCode;
            base.ClosePrice1 = 0;
            base.Volume = volume;
            base.DTime = dt;
        }
    }
}
