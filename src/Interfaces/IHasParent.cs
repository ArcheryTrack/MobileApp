using System;

namespace ATMobile.Interfaces
{
    public interface IHasParent
    {
        Guid ParentId { get; set; }
    }
}

