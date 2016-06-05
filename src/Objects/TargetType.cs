﻿using System;

namespace ATMobile.Objects
{
    public class TargetType : AbstractObject
    {
        public TargetType()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public Distance Size { get; set; }
    }
}
