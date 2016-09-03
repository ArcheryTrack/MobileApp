using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ATMobile.Interfaces
{
    public interface IMainForm
    {
        Task<bool> ShowAlert (string _title, string _message, string _accept, string _cancel);

        void PushModal (ContentPage _form);
    }
}

