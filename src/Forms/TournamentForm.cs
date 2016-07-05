using System;
using ATMobile.Constants;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class TournamentForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;

        private Button m_btnAddArcher;

        public TournamentForm ()
        {
            Title = "Tournament";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex (UIConstants.FormBackgroundColor);

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_InsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            m_OutsideLayout.Children.Add (m_InsideLayout);

            m_btnAddArcher = new Button {
                Text = "Add Archer"
            };
            m_btnAddArcher.Clicked += AddArcher;
            m_OutsideLayout.Children.Add (m_btnAddArcher);


            Content = m_OutsideLayout;
        }

        async private void AddArcher (object sender, EventArgs e)
        {
            ArcherPicker picker = new ArcherPicker ();

            await Navigation.PushModalAsync (picker);
        }

        public void SetupForm (Tournament _tournament)
        {

        }
    }
}

