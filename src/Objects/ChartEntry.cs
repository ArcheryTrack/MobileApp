using System;
using ATMobile.Enums;

namespace ATMobile.Objects
{
    public class ChartEntry : AbstractObject
    {
        public ChartEntryTypes ChartEntryType { get; set; }

        public Guid? ParentId { get; set; }

        private DateTime? m_Date;

        public int? Sequence { get; set; }

        public Guid ArcherId { get; set; }

        public DateTime? Date {
            get {
                return m_Date;
            }
            set {
                if (value != null) {
                    m_Date = value.Value;
                } else {
                    m_Date = null;
                }
            }
        }

        public int Count { get; set}

        public double Score { get; set; }

        public double Possible { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public double Mean {
            get {
                if (Count > 0) {
                    return Score / Count;
                } else {
                    return 0;
                }
            }
        }

        public double Percentage {
            get {
                if (Possible > 0) {
                    return Score / Possible;
                } else {
                    return 0;
                }
            }
        }
    }
}

