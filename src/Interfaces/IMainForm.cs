using System;
using System.Threading.Tasks;

namespace ATMobile.Interfaces
{
    public interface IMainForm
    {
        Task ShowAlert (string _title, string _message, string _accept, string _cancel);
    }
}

