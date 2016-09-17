using Xamarin.Forms;

namespace ATMobile.Controls
{
    public abstract class AbstractListView : ListView
    {
        public AbstractListView ()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;
            HasUnevenRows = false;
        }
    }
}

