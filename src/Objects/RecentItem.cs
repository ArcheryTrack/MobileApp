using System;
using ATMobile.Helpers;

namespace ATMobile.Objects
{
    public class RecentItem
    {
        public Guid Id { get; set; }

        public string ItemType { get; set; }

        public string Archers { get; set; }

        public int ArcherCount { get; set; }

        public DateTime Date { get; set; }

        public string RecentTypeText {
            get {
                string temp = string.Format ("{0} - {1}", ItemType, Date.ToDisplayDate ());
                return temp;
            }
        }

        public string ArcherText {
            get {
                string temp = null;

                if (ArcherCount > 1) {
                    temp = string.Format ("Archers - {0}", Archers);
                } else {
                    temp = string.Format ("Archer - {0}", Archers);
                }

                return temp;
            }
        }
    }
}

