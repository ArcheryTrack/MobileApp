using System;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Forms
{
    public class PracticeEndsForm : ContentPage
    {
        private Archer m_Archer;
        private Practice m_Practice;

        public PracticeEndsForm ()
        {
        }

        public void SetupForm (Archer _archer, Practice _practice)
        {
            m_Archer = _archer;
            m_Practice = _practice;

            if (m_Practice != null) {

            }
        }
    }
}

