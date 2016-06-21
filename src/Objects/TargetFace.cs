using System;
namespace ATMobile.Objects
{
    public class TargetFace : AbstractObject
    {
        public string Name { get; set; }

        public int MinimumPoints { get; set; }

        public int MaximumPoints { get; set; }

        public Distance Size { get; set; }
    }
}

