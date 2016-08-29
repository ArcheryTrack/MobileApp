using System;
using ATMobile.Controls;
using Xamarin.Forms;

namespace ATMobile.Cells
{
    public class SightSettingCell : ViewCell
    {
        private StackLayout m_Layout;
        private ATLabel m_lblDistance;
        private ATLabel m_lblSetting;

        public SightSettingCell ()
        {
            m_Layout = new StackLayout ();
            m_Layout.Padding = new Thickness (0, 5);

            m_lblDistance = new ATLabel ();
            m_lblDistance.SetBinding (Label.TextProperty, "DistanceText");
            m_Layout.Children.Add (m_lblDistance);

            m_lblSetting = new ATLabel ();
            m_lblSetting.SetBinding (Label.TextProperty, "Setting");
            m_Layout.Children.Add (m_lblSetting);

            View = m_Layout;
        }
    }
}

