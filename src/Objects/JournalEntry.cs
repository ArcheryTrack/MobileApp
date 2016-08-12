using System;
using ATMobile.Helpers;
using ATMobile.Interfaces;

namespace ATMobile.Objects
{
    public class JournalEntry : AbstractObject, IHasParent
    {
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Archer
        /// </summary>
        /// <value>The parent identifier.</value>
        public Guid ParentId { get; set; }

        public string EntryText { get; set; }

        public string DisplayText {
            get {
                string text = "";

                if (EntryText == null) {
                    text = "[Nothing entered]";
                } else {
                    if (EntryText.Length > 30) {
                        text = EntryText.Substring (0, 26) + " ...";
                    } else {
                        text = EntryText;
                    }
                }

                return string.Format ("{0} - {1}",
                                     DateTime.ToDisplayDateTime (),
                                     text);
            }
        }
    }
}

