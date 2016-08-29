using System;
using System.Text;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentTypeForm : AbstractEntryForm
    {
        private TournamentType m_TournamentType;

        private ATTextWithLabel m_txtName;
        private ATTextWithLabel m_txtDescription;
        private RoundTypeListView m_RoundTypes;
        private Button m_btnAddRound;

        public TournamentTypeForm () : base ("Tournament Type Setup")
        {
            m_txtName = new ATTextWithLabel {
                Title = "Name",
                Placeholder = "Enter a name for the tournament type"
            };
            InsideLayout.Children.Add (m_txtName);

            m_txtDescription = new ATTextWithLabel {
                Title = "Description",
                Placeholder = "Enter a description"
            };
            InsideLayout.Children.Add (m_txtDescription);

            m_btnAddRound = new Button {
                Text = "Add Round"
            };
            m_btnAddRound.Clicked += OnAddRound;
            InsideLayout.Children.Add (m_btnAddRound);

            m_RoundTypes = new RoundTypeListView ();
            m_RoundTypes.ItemSelected += OnSelectedRound;
            Frame frame = new Frame {
                HasShadow = false,
                MinimumHeightRequest = 240,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            frame.Content = m_RoundTypes;
            InsideLayout.Children.Add (frame);
        }

        public void SetupForm (TournamentType _tournamentType)
        {
            if (_tournamentType == null) {
                m_TournamentType = new TournamentType ();
            } else {
                m_TournamentType = _tournamentType;
            }

            m_txtName.Text = m_TournamentType.Name;
            m_txtDescription.Text = m_TournamentType.Description;

            m_RoundTypes.RefreshList (m_TournamentType.Id);
        }

        void OnSelectedRound (object sender, SelectedItemChangedEventArgs e)
        {
            SaveTournamentType ();

            RoundType roundType = (RoundType)m_RoundTypes.SelectedItem;

            RoundTypeForm form = new RoundTypeForm ();
            form.SetupForm (roundType);

            PublishActionMessage ("RoundType Selected");

            Navigation.PushModalAsync (form);
        }

        private void SaveTournamentType ()
        {
            m_TournamentType.Name = m_txtName.Text;
            m_TournamentType.Description = m_txtDescription.Text;
            ATManager.GetInstance ().Persist (m_TournamentType);
        }

        void OnAddRound (object sender, EventArgs e)
        {
            SaveTournamentType ();

            RoundType roundType = new RoundType ();
            roundType.ParentId = m_TournamentType.Id;

            RoundTypeForm form = new RoundTypeForm ();
            form.SetupForm (roundType);

            PublishActionMessage ("Add Round Selected");

            Navigation.PushModalAsync (form);
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            if (m_txtName.Text == null) {
                _sb.AppendLine ("You must specify a name for the tournament type.");
            }
        }

        public override void Save ()
        {
            SaveTournamentType ();
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_RoundTypes.RefreshList (m_TournamentType.Id);
        }
    }
}

