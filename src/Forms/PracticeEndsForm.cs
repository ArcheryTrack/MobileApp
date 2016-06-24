using System;
using ATMobile.Controls;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndsForm : ContentPage
    {
        private Archer m_Archer;
        private Practice m_Practice;

        private StackLayout m_OutsideLayout;
        private Button m_btnAdd;
        private Label m_lblArcher;
        private PracticeEndsListView m_PracticeEnds;

        public PracticeEndsForm ()
        {
            Title = "Practice";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex ("EEEEEE");

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_lblArcher = new Label ();
            m_OutsideLayout.Children.Add (m_lblArcher);

            m_btnAdd = new Button {
                Text = "Add End"
            };
            m_btnAdd.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_btnAdd);

            //Add the sight settings listview
            m_PracticeEnds = new PracticeEndsListView ();
            m_PracticeEnds.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_PracticeEnds);

            Content = m_OutsideLayout;
        }

        public void SetupForm (Archer _archer, Practice _practice)
        {
            m_Archer = _archer;
            m_Practice = _practice;

            m_lblArcher.Text = m_Archer.FullName;

            if (m_Practice != null) {
                m_PracticeEnds.RefreshList (m_Practice.Id);
            }
        }

        void OnAdd (object sender, EventArgs e)
        {
            PracticeEndForm practiceEnd = new PracticeEndForm ();
            practiceEnd.SetupForm (m_Archer, m_Practice, null, m_PracticeEnds.Ends.Count);
            Navigation.PushAsync (practiceEnd);
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            PracticeEnd end = (PracticeEnd)e.SelectedItem;

            PracticeEndForm practiceEnd = new PracticeEndForm ();
            practiceEnd.SetupForm (m_Archer, m_Practice, end, m_PracticeEnds.Ends.Count);
            Navigation.PushAsync (practiceEnd);
        }

        protected override void OnAppearing ()
        {
            RefreshList ();
        }

        private void RefreshList ()
        {
            m_PracticeEnds.RefreshList (m_Practice.Id);
        }
    }
}

