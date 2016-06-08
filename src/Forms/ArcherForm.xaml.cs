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

        public ArcherForm()
        {
            InitializeComponent();

            Title = "Archer";
        }

        public void SetArcher(Archer _archer)
        {
            m_Archer = _archer;

            txtFirstName.Text = _archer.FirstName;
            txtLastName.Text = _archer.LastName;
            datBirthdate.Date = _archer.BirthDate;
            datStartedArchery.Date = _archer.StartedArchery;
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (m_Archer == null)
            {
                m_Archer = new Archer();
                m_Archer.Guid = Guid.NewGuid();
            }
        
            m_Archer.FirstName = txtFirstName.Text;
            m_Archer.LastName = txtLastName.Text;
            m_Archer.BirthDate = datBirthdate.Date;
            m_Archer.StartedArchery = datStartedArchery.Date;

            ATManager.GetInstance().Persist(m_Archer);

            Navigation.PopAsync();
        }
    }
}

