using ATMobile.Cells;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class MatchListView : AbstractListView
    {
        public MatchListView ()
        {
            ItemTemplate = new DataTemplate (typeof (MatchCell));
            //RowHeight = 30;
            HasUnevenRows = true;
        }
    }
}

