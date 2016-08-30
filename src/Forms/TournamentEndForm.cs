using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentEndForm : AbstractEntryForm
    {
        private Tournament m_Tournament;
        private Round m_Round;
        private TournamentEnd m_TournamentEnd;
        private TargetFace m_TargetFace;

        private ATLabel m_lblTournament;
        private ATLabel m_lblEnd;
        private StackLayout m_ArcherLayout;
        private Button m_btnPrevious;
        private Button m_btnNext;
        private ATLabel m_lblArcher;

        private ATLabel m_lblNote;
        private Editor m_txtNote;

        private ATLabel m_lblPoints;

        private ShotArrowListView m_ArrowsListView;
        private ScoreControl m_ScoreControl;

        private int m_CurrentArcherIndex;
        private Archer m_CurrentArcher;
        private int m_EndNumber;

        public TournamentEndForm () : base ("End")
        {
            OutsideLayout.Spacing = 5;
            InsideLayout.Spacing = 5;

            m_lblTournament = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            InsideLayout.Children.Add (m_lblTournament);

            m_lblEnd = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            InsideLayout.Children.Add (m_lblEnd);

            /* Setup the Archer */
            m_ArcherLayout = new StackLayout {
                Spacing = 5,
                Padding = 5,
                Orientation = StackOrientation.Horizontal
            };
            InsideLayout.Children.Add (m_ArcherLayout);

            m_btnPrevious = new Button {
                Text = "<",
                HorizontalOptions = LayoutOptions.Start
            };
            m_btnPrevious.Clicked += PreviousClicked;
            m_ArcherLayout.Children.Add (m_btnPrevious);

            m_lblArcher = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            m_ArcherLayout.Children.Add (m_lblArcher);

            m_btnNext = new Button {
                Text = ">",
                HorizontalOptions = LayoutOptions.End
            };
            m_btnNext.Clicked += NextClicked;
            m_ArcherLayout.Children.Add (m_btnNext);

            m_lblPoints = new ATLabel {
                Text = "Points: "
            };
            InsideLayout.Children.Add (m_lblPoints);

            StackLayout layout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 5,
                Spacing = 5
            };
            InsideLayout.Children.Add (layout);

            Frame frame = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            m_ArrowsListView = new ShotArrowListView ();
            frame.Content = m_ArrowsListView;
            layout.Children.Add (frame);

            Frame frame2 = new Frame {
                HasShadow = false,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            m_ScoreControl = new ScoreControl ();
            m_ScoreControl.ScoreClicked += Clicked;
            frame2.Content = m_ScoreControl;
            layout.Children.Add (frame2);

            m_lblNote = new ATLabel {
                Text = "Note",
                Margin = new Thickness (0, 10, 0, 5)
            };
            InsideLayout.Children.Add (m_lblNote);

            m_txtNote = new Editor {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            InsideLayout.Children.Add (m_txtNote);
        }

        void PreviousClicked (object sender, EventArgs e)
        {
            SaveEnd ();

            m_CurrentArcherIndex--;

            if (m_CurrentArcherIndex < 0) {
                m_CurrentArcherIndex = m_Tournament.Archers.Count - 1;
            }

            SetArcher ();
        }

        void NextClicked (object sender, EventArgs e)
        {
            SaveEnd ();

            m_CurrentArcherIndex++;

            if (m_CurrentArcherIndex >= m_Tournament.Archers.Count) {
                m_CurrentArcherIndex = 0;
            }

            SetArcher ();
        }

        private void SetArcher ()
        {
            Guid archerId = m_Tournament.Archers [m_CurrentArcherIndex];
            m_CurrentArcher = ATManager.GetInstance ().GetArcher (archerId);

            if (m_Tournament.Archers.Count <= 1) {
                m_btnNext.IsEnabled = false;
                m_btnPrevious.IsEnabled = false;
            } else {
                m_btnNext.IsEnabled = true;
                m_btnPrevious.IsEnabled = true;
            }

            m_lblArcher.Text = m_CurrentArcher.FullName;

            LoadEnd (archerId, m_EndNumber);
        }

        public int CurrentArcherIndex {
            get {
                return m_CurrentArcherIndex;
            }
        }

        public Archer CurrentArcher {
            get {
                return m_CurrentArcher;
            }
        }

        private void LoadEnd (Guid _archerId, int _endNumber)
        {
            TournamentEnd end = ATManager.GetInstance ().GetTournamentEnd (m_Round.Id, _archerId, _endNumber);

            if (end != null) {
                m_TournamentEnd = end;

                SetupEnd ();
            }
        }

        private int FindNextArrowNumber ()
        {
            var sorted = m_ArrowsListView.Arrows;

            int count = sorted.Count;

            for (int i = 0; i < count; i++) {
                ShotArrow arrow = sorted [i];

                if (arrow.ArrowNumber != i + 1) {
                    return i + 1;
                }
            }

            return count + 1;
        }

        private void Clicked (string value)
        {
            ShotArrow arrow = new ShotArrow ();

            arrow.ArrowNumber = FindNextArrowNumber ();
            arrow.Score = value;
            arrow.PossiblePoints = m_TargetFace.MaximumPoints;

            if (value == "X") {
                arrow.ScoreValue = m_TargetFace.MaximumPoints;
                arrow.SortValue = arrow.ScoreValue + 1;
            } else if (value == "M") {
                arrow.ScoreValue = 0;
                arrow.SortValue = 0;
            } else {
                arrow.ScoreValue = Convert.ToInt32 (value);
                arrow.SortValue = arrow.ScoreValue;
            }

            m_ArrowsListView.Arrows.Insert (arrow.ArrowNumber - 1, arrow);

            SetPoints ();
        }

        private void SetPoints ()
        {
            int points = 0;
            foreach (var item in m_ArrowsListView.Arrows) {
                points += item.ScoreValue;
            }
            m_lblPoints.Text = string.Format ("Points: {0}", points);
        }

        public void FillList ()
        {
            ObservableCollection<ShotArrow> arrows = m_ArrowsListView.Arrows;
            arrows.Clear ();

            foreach (var item in m_TournamentEnd.SortedByArrowNumber) {
                arrows.Add (item);
            }

            m_ArrowsListView.Arrows = arrows;
        }

        public void SetupForm (
            Archer _archer,
            Tournament _tournament,
            Round _round,
            TournamentEnd _end)
        {
            m_Tournament = _tournament;
            m_Round = _round;
            m_TournamentEnd = _end;
            m_EndNumber = _end.EndNumber;

            m_lblTournament.Text = _tournament.Name;
            m_lblEnd.Text = string.Format ("Round {0}, End {1}", _round.RoundNumber, _end.EndNumber);

            SetArcher ();

            ATManager manager = ATManager.GetInstance ();
            m_TargetFace = manager.GetTargetFace (m_Round.TargetFaceId);
        }

        private void SetupEnd ()
        {
            FillList ();

            m_txtNote.Text = m_TournamentEnd.Note;

            m_ScoreControl.SetTargetFace (m_TargetFace);

            SetPoints ();
        }

        public void SaveEnd ()
        {
            m_TournamentEnd.Note = m_txtNote.Text;
            m_TournamentEnd.Results = m_ArrowsListView.Arrows.ToList ();
            ATManager.GetInstance ().Persist (m_TournamentEnd);
        }

        public override void ValidateForm (StringBuilder _sb)
        {
            List<ShotArrow> arrows = m_ArrowsListView.Arrows.ToList ();

            if (arrows.Count > m_Round.ArrowsPerEnd) {
                _sb.AppendFormat ("Only {0} arrows were expected for this end, but {1} were recorded.",
                                  m_Round.ArrowsPerEnd,
                                  arrows.Count);
            }
        }

        public override void Save ()
        {
            SaveEnd ();
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();

            m_ScoreControl.ScoreClicked -= Clicked;
            m_ArrowsListView.Dispose ();
        }
    }
}

