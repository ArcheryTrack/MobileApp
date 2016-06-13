using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ATMobile.Controls;
using ATMobile.Objects;

namespace ATMobile.Forms
{
    public partial class ArchersForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private Button m_Add;
        private ArcherListView m_ArcheryList;

        public ArchersForm ()
        {
            InitializeComponent ();

            Title = "Archers";

            //Icon = "settings.png";
            BackgroundColor = Color.FromHex ("EEEEEE");

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_Add = new Button {
                Text = "Add Archer"
            };
            m_Add.Clicked += OnAdd;
            m_OutsideLayout.Children.Add (m_Add);

            m_ArcheryList = new ArcherListView ();
            m_ArcheryList.ItemSelected += OnSelected;
            m_OutsideLayout.Children.Add (m_ArcheryList);

            Content = m_OutsideLayout;
        }

        void OnSelected (object sender, SelectedItemChangedEventArgs e)
        {
            Archer archer = (Archer)e.SelectedItem;

            ArcherForm addArcher = new ArcherForm ();
            addArcher.SetArcher (archer);

            Navigation.PushAsync (addArcher);
        }

        void OnAdd (object sender, EventArgs e)
        {
            ArcherForm addArcher = new ArcherForm ();
            Navigation.PushAsync (addArcher);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            m_ArcheryList.RefreshList ();
        }
    }
}

