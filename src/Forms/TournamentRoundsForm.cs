using System;
using ATMobile.Cells;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentRoundsForm : AbstractForm, IDisposable
    {
        private Tournament m_Tournament;

        TournamentRoundsListView m_listRounds;
        Label m_lblTournamentTitle;


        public TournamentRoundsForm () : base ("Rounds")
        {
            m_lblTournamentTitle = new Label {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            OutsideLayout.Children.Add (m_lblTournamentTitle);

            Frame frame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            m_listRounds = new TournamentRoundsListView ();
            m_listRounds.ItemSelected += OnSelected;
            frame.Content = m_listRounds;

            OutsideLayout.Children.Add (frame);

            TournamentRoundCell.RoundEditClicked += EditRound;
        }

        public void EditRound (Round _round)
        {
            TournamentRoundForm editRound = new TournamentRoundForm ();
            editRound.SetupForm (m_Tournament, _round);
            Navigation.PushAsync (editRound);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Round round = (Round)e.SelectedItem;

            TournamentEndsForm tournamentEnds = new TournamentEndsForm ();
            tournamentEnds.SetupForm (m_Tournament, round);
            Navigation.PushAsync (tournamentEnds);
        }

        public void SetupForm (Tournament _tournament)
        {
            m_Tournament = _tournament;

            if (_tournament.Name != null) {
                m_lblTournamentTitle.Text = _tournament.Name;
            } else {
                m_lblTournamentTitle.Text = "[Name not specified]";
            }
            m_listRounds.RefreshList (_tournament.Id);
        }

        public void Dispose ()
        {
            TournamentRoundCell.RoundEditClicked -= EditRound;
        }
    }
}

