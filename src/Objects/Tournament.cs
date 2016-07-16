using System;
using System.Collections.Generic;

namespace ATMobile.Objects
{
    public class Tournament : AbstractObject
    {
        public Tournament ()
        {
            Archers = new List<Guid> ();
        }

        public string Name { get; set; }

        public Guid? RangeId { get; set; }

        public Guid? TournamentTypeId { get; set; }

        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public List<Guid> Archers { get; set; }

        public string NameString {
            get {
                if (Name == null) {
                    return "Name not set";
                }

                return Name;
            }
        }

        public string DateTimeString {
            get {
                if (StartDateTime == null
                    && EndDateTime == null) {
                    return "Date not set";
                }

                if (StartDateTime != null
                    && EndDateTime == null) {
                    return StartDateTime.Value.ToString ("d");
                }

                if (StartDateTime == null
                    && EndDateTime != null) {
                    return EndDateTime.Value.ToString ("d");
                }

                if (StartDateTime.Equals (EndDateTime)) {
                    return StartDateTime.Value.ToString ("d");
                }

                return StartDateTime.Value.ToString ("d") + " - " + EndDateTime.Value.ToString ("d");
            }
        }
    }
}

