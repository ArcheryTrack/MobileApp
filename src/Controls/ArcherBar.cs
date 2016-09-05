using System;
using System.Collections.Generic;
using ATMobile.Delegates;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ArcherBar : ContentView
    {
        private Archer m_CurrentArcher;
        private int m_CurrentArcherIndex;
        private List<Archer> m_Archers;
        private StackLayout m_ArcherLayout;
        private Button m_btnPrevious;
        private Button m_btnNext;
        private ATLabel m_lblArcher;

        public event ArcherPickedDelegate ArcherPicked;

        public ArcherBar ()
        {
            m_Archers = ATManager.GetInstance ().GetArchers ();

            Guid? archerId = ATManager.GetInstance ().SettingManager.GetCurrentArcher ();
            if (archerId.HasValue) {
                m_CurrentArcherIndex = FindArcherIndex (archerId.Value);
            } else {
                m_CurrentArcherIndex = 0;
            }

            /* Setup the Archer */
            m_ArcherLayout = new StackLayout {
                Spacing = 5,
                Padding = 5,
                Orientation = StackOrientation.Horizontal
            };
            Content = m_ArcherLayout;

            m_btnPrevious = new Button {
                Text = "<",
                HorizontalOptions = LayoutOptions.Start
            };
            m_btnPrevious.Clicked += PreviousClicked;
            m_ArcherLayout.Children.Add (m_btnPrevious);

            m_lblArcher = new ATLabel {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            m_ArcherLayout.Children.Add (m_lblArcher);

            m_btnNext = new Button {
                Text = ">",
                HorizontalOptions = LayoutOptions.End
            };
            m_btnNext.Clicked += NextClicked;
            m_ArcherLayout.Children.Add (m_btnNext);

            SetArcher ();
        }

        public Archer CurrentArcher {
            get {
                return m_CurrentArcher;
            }

            set {
                m_CurrentArcher = value;

                if (value != null) {
                    m_CurrentArcherIndex = FindArcherIndex (m_CurrentArcher.Id);
                } else {
                    m_CurrentArcherIndex = 0;
                }

                SetArcher ();
            }
        }

        private int FindArcherIndex (Guid _archerId)
        {
            int index = 0;

            for (int i = 0; i < m_Archers.Count; i++) {
                if (m_Archers [i].Id.Equals (_archerId)) {
                    index = i;
                }
            }

            return index;
        }

        public List<Archer> Archers {
            get {
                return m_Archers;
            }

            set {
                m_Archers = value;

                SetArcher ();
            }
        }

        private void SetArcher ()
        {
            if (m_CurrentArcherIndex + 1 > m_Archers.Count) {
                m_CurrentArcherIndex = 0;
            }
            m_CurrentArcher = m_Archers [m_CurrentArcherIndex];

            ATManager.GetInstance ().SettingManager.SetCurrentArcher (m_CurrentArcher.Id);

            m_lblArcher.Text = m_CurrentArcher.FullName;

            var picked = ArcherPicked;
            if (picked != null) {
                picked (m_CurrentArcher);
            }
        }

        void PreviousClicked (object sender, EventArgs e)
        {
            m_CurrentArcherIndex--;

            if (m_CurrentArcherIndex < 0) {
                m_CurrentArcherIndex = m_Archers.Count - 1;
            }

            SetArcher ();
        }

        void NextClicked (object sender, EventArgs e)
        {
            m_CurrentArcherIndex++;

            if (m_CurrentArcherIndex >= m_Archers.Count) {
                m_CurrentArcherIndex = 0;
            }

            SetArcher ();
        }
    }
}

