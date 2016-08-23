using System;
using System.Text;

namespace ATMobile.Objects
{
    public class Archer : AbstractObject
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? StartedArchery { get; set; }

        public string FullName {
            get {
                StringBuilder sb = new StringBuilder ();

                if (FirstName != null) {
                    sb.Append (FirstName);
                }

                if (sb.Length > 0
                    && LastName != null) {
                    sb.Append (" ");
                }

                sb.Append (LastName);

                return sb.ToString ();
            }
        }
    }
}

