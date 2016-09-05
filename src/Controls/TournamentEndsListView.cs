using ATMobile.Cells;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentEndsListView : AbstractListView
    {
        public TournamentEndsListView ()
        {
            ItemTemplate = new DataTemplate (typeof (TournamentEndCell));
            //RowHeight = 30;
            HasUnevenRows = true;
        }
    }
}

