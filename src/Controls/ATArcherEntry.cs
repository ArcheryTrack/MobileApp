﻿using System;
using System.Collections.Generic;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATArcherEntry : ContentView
    {
        private string m_strLongDescription;
        private string m_strShortDescription;
        private ATLabel m_lblTitle;
        private Grid m_gridLayout;
        private Button m_btnPick;
        private Archer m_Archer;
        private List<Guid> m_PossibleArchers = null;

        public ATArcherEntry (string _shortDescription, string _longDescription)
        {
            m_strShortDescription = _shortDescription;
            m_strLongDescription = _longDescription;

            //Setup grid to hold the controls
            m_gridLayout = new Grid {
                Padding = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition {
                        Height = new GridLength (40, GridUnitType.Absolute) //Name
                    }
                },
                ColumnDefinitions = {
                    new ColumnDefinition {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition {
                        Width = new GridLength(80, GridUnitType.Absolute)
                    }
                }
            };

            m_lblTitle = new ATLabel {
                VerticalTextAlignment = TextAlignment.Center
            };

            if (Device.Idiom == TargetIdiom.Phone) {
                m_lblTitle.Text = m_strShortDescription;
            } else {
                m_lblTitle.Text = m_strLongDescription;
            }

            m_gridLayout.Children.Add (m_lblTitle, 0, 0);

            m_btnPick = new Button {
                Text = "Pick",
                WidthRequest = 80,
                HeightRequest = 40,
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End
            };
            m_btnPick.Clicked += Pick;
            m_gridLayout.Children.Add (m_btnPick, 1, 0);

            Content = m_gridLayout;
        }

        async private void Pick (object sender, EventArgs e)
        {
            ArcherPicker picker = new ArcherPicker (m_PossibleArchers, false);
            picker.ItemPicked += Picked;

            await Navigation.PushModalAsync (picker);
        }

        private void Picked (Archer _archer)
        {
            m_Archer = _archer;
            SetText ();
        }

        private void SetText ()
        {
            if (m_Archer == null) {
                if (Device.Idiom == TargetIdiom.Phone) {
                    m_lblTitle.Text = m_strShortDescription;
                } else {
                    m_lblTitle.Text = m_strLongDescription;
                }

            } else {
                m_lblTitle.Text = string.Format (
                    "{0} : {1}",
                    m_strShortDescription,
                    m_Archer.FullName);
            }
        }

        public Archer Archer {
            get {
                return m_Archer;
            }

            set {
                m_Archer = value;
                SetText ();
            }
        }

        public new bool IsEnabled {
            get {
                return base.IsEnabled;
            }

            set {
                base.IsEnabled = value;
                m_btnPick.IsEnabled = value;
            }
        }

        public List<Guid> PossibleArchers {
            get {
                return m_PossibleArchers;
            }
            set {
                m_PossibleArchers = value;
            }
        }
    }
}

