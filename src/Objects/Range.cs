using System;
using System.Text;

namespace ATMobile.Objects
{
    public class Range : AbstractObject
    {
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Location {
            get {
                StringBuilder sb = new StringBuilder ();

                if (City != null) {
                    sb.Append (City);
                }

                if (sb.Length > 0
                    && !string.IsNullOrEmpty (State)) {
                    sb.Append (", ");
                }

                if (!string.IsNullOrEmpty (State)) {
                    sb.Append (State);
                }

                if (sb.Length > 0
                    && !string.IsNullOrEmpty (Country)) {
                    sb.Append (" ");
                }

                if (!string.IsNullOrEmpty (Country)) {
                    sb.Append (Country);
                }

                return sb.ToString ();
            }
        }
    }
}

