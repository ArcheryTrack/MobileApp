using System;
using ATMobile.Enums;

namespace ATMobile.Objects
{
    public class ChartEntry : AbstractObject
    {
        public ChartEntryTypes ChartEntryType { get; set; }

        public Guid? ParentId { get; set; }

        private DateTime m_Date;

        public int Sequence { get; set; }

        public Guid ArcherId { get; set; }

        public DateTime Date {
            get {
                return m_Date;
            }
            set {
                m_Date = value.Date;
            }
        }

        public int Count { get; set}

        public Double Average { get; set; }

        public Double Total { get; set; }

        public Double Minimum { get; set; }

        public Double Maximum { get; set; }
    }
}

