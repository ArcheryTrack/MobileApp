using System;

namespace ATMobile.Objects
{
    public class Setting : AbstractObject
    {
        public Setting()
        {
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}

