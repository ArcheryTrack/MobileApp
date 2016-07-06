using ATMobile.Cells;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class GenericListView<T> : ListView where T : AbstractBaseCell
    {
        public GenericListView ()
        {
            ItemTemplate = new DataTemplate (typeof (T));
        }
    }
}

