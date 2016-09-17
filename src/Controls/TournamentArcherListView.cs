using ATMobile.Cells;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class TournamentArcherListView : AbstractListView
    {
        public TournamentArcherListView ()
        {
            ItemTemplate = new DataTemplate (typeof (ArcherCell));
        }
    }
}

