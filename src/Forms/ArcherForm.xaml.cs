using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ATMobile.Objects;
using ATMobile.Managers;

namespace ATMobile.Forms
{
    public partial class ArcherForm : ContentPage
    {
        private Archer m_Archer;
        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;
        private Entry m_txtFirstName;
        private Entry m_txtLastName;
        private Label m_lblBirthDate;
        private Label m_lblStartedArchery;
        private DatePicker m_datBirthdate;
        private DatePicker m_datStartedArchery;
        private Button m_btnSave;

        public ArcherForm ()
        {
            InitializeComponent ();

            Title = "Archer";

            m_OutsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };

            m_btnSave = new Button {
                Text = "Save"
            };
            m_btnSave.Clicked += OnSave;
            m_OutsideLayout.Children.Add (m_btnSave);

            m_InsideLayout = new StackLayout {
                Spacing = 15,
                VerticalOptions = LayoutOptions.Fill,
                Padding = 5
            };
            m_OutsideLayout.Children.Add (m_InsideLayout);

            m_txtFirstName = new Entry ();
            m_txtFirstName.Placeholder = "First Name";
            m_InsideLayout.Children.Add (m_txtFirstName);

            m_txtLastName = new Entry ();
            m_txtLastName.Placeholder = "Last Name";
            m_InsideLayout.Children.Add (m_txtLastName);

            m_lblBirthDate = new Label ();
            m_lblBirthDate.Text = "Birth Date";
            m_InsideLayout.Children.Add (m_lblBirthDate);

            m_datBirthdate = new DatePicker ();
            m_InsideLayout.Children.Add (m_datBirthdate);

            m_lblStartedArchery = new Label ();
            m_lblStartedArchery.Text = "Started Archery On";
            m_InsideLayout.Children.Add (m_lblStartedArchery);

            m_datStartedArchery = new DatePicker ();
            m_InsideLayout.Children.Add (m_datStartedArchery);

            Content = m_OutsideLayout;
        }

        public void SetArcher (Archer _archer)
        {
            m_Archer = _archer;

            m_txtFirstName.Text = _archer.FirstName;
            m_txtLastName.Text = _archer.LastName;
            m_datBirthdate.Date = _archer.BirthDate;
            m_datStartedArchery.Date = _archer.StartedArchery;
        }

        private void OnSave (object sender, EventArgs e)
        {
            if (m_Archer == null) {
                m_Archer = new Archer ();
                m_Archer.Id = Guid.NewGuid ();
            }

            m_Archer.FirstName = m_txtFirstName.Text;
            m_Archer.LastName = m_txtLastName.Text;
            m_Archer.BirthDate = m_datBirthdate.Date;
            m_Archer.StartedArchery = m_datStartedArchery.Date;

            ATManager.GetInstance ().Persist (m_Archer);

            Navigation.PopAsync ();
        }


    }
}

