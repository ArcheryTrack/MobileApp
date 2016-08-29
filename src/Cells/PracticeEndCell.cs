﻿using System;
using ATMobile.Controls;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class PracticeEndCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblEndNumber;
        private ATLabel m_lblScore;
        private ATLabel m_lblTotal;

        public PracticeEndCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Orientation = StackOrientation.Horizontal;
            m_Layout.Padding = new Thickness (0, 5);

            m_Layout.Children.Add (new ATLabel () { Text = "End " });

            m_lblEndNumber = new ATLabel ();
            m_lblEndNumber.SetBinding (Label.TextProperty, "EndNumber");
            m_Layout.Children.Add (m_lblEndNumber);

            m_Layout.Children.Add (new ATLabel () { Text = ":" });

            m_lblScore = new ATLabel ();
            m_lblScore.SetBinding (Label.TextProperty, "ResultsString");
            m_Layout.Children.Add (m_lblScore);

            m_Layout.Children.Add (new ATLabel () { Text = " = " });

            m_lblTotal = new ATLabel ();
            m_lblTotal.SetBinding (Label.TextProperty, "TotalScore");
            m_Layout.Children.Add (m_lblTotal);

            View = m_Layout;
        }
    }
}

