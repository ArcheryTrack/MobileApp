using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ATMobile.Forms
{
    public partial class TournamentTypeSetupForm : ContentPage
    {
        private StackLayout m_OutsideLayout;
        private StackLayout m_InsideLayout;

        private Button m_btnSave;

        private Entry m_txtName;

        private Button m_btnAddRound;
        //List of rounds




        public TournamentTypeSetupForm ()
        {
            Title = "Tournament Type Setup";
        }
    }
}

