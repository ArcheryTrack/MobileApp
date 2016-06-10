using System;
using System.Collections.Generic;
using ATMobile.Controls;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class SightSetupForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_Add;
        private Picker m_ArcherPicker;
        private SightSettingListView m_SightSettings;

        private List<Archer> m_Archers;

        public SightSetupForm ()
        {
            InitializeComponent ();

            Title = "Sight Setup";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex ("EEEEEE");

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_Add = new Button {
                Text = "Add Setting"
            };
            m_Add.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_Add);

            //Load Archers and setup picker
            m_Archers = ATManager.GetInstance ().GetArchers ();

            m_ArcherPicker = new Picker ();
            foreach (var archer in m_Archers) {
                m_ArcherPicker.Items.Add (archer.FullName);
            }
            m_ArcherPicker.SelectedIndexChanged += OnArcherPicked;
            m_OutsideLayout.Children.Add (m_ArcherPicker);

            //Add the sight settings listview
            m_SightSettings = new SightSettingListView ();
            m_SightSettings.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_SightSettings);

            Content = m_OutsideLayout;
        }

        void OnArcherPicked (object sender, EventArgs e)
        {
            if (m_ArcherPicker.SelectedIndex == -1) {
                m_SightSettings.ClearList ();
            } else {
                Archer archer = m_Archers [m_ArcherPicker.SelectedIndex];
                m_SightSettings.RefreshList (archer.Guid);
            }
        }

        void OnAdd (object sender, EventArgs e)
        {
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}

