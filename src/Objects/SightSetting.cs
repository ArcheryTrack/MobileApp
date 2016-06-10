﻿using System;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class SightSetting : AbstractObject, IHasParent
    {
        public DateTime DateTime { get; set; }

        public Distance Distance { get; set; }

        public double Setting { get; set; }

        public Guid ParentGuid { get; set; }
    }
}

