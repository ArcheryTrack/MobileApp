﻿using System;
using ATMobile.Objects;
using ATMobile.PickerForms;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public class ATRangeEntry : ContentView
    {
        private string m_strLongDescription;
        private string m_strShortDescription;
        private ATLabel m_lblTitle;
        private Grid m_gridLayout;
        private Button m_btnPick;
        private Range m_Range;

        public ATRangeEntry (string _shortDescription, string _longDescription)
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
            RangePicker picker = new RangePicker ();
            picker.ItemPicked += Picked;

            await Navigation.PushModalAsync (picker);
        }

        private void Picked (Range _range)
        {
            m_Range = _range;
            SetText ();
        }

        private void SetText ()
        {
            if (m_Range == null) {
                if (Device.Idiom == TargetIdiom.Phone) {
                    m_lblTitle.Text = m_strShortDescription;
                } else {
                    m_lblTitle.Text = m_strLongDescription;
                }

            } else {
                m_lblTitle.Text = string.Format (
                    "{0} : {1}",
                    m_strShortDescription,
                    m_Range.Name);
            }
        }

        public Range Range {
            get {
                return m_Range;
            }

            set {
                m_Range = value;
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
    }
}

