using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ATMobile.Cells;
using ATMobile.Delegates;
using ATMobile.Managers;
using ATMobile.Objects;
using Xamarin.Forms;
using System.Linq;

namespace ATMobile.Controls
{
    public class ShotArrowListView : AbstractListView, IDisposable
    {
        private ObservableCollection<ShotArrow> m_Arrows;

        public ObservableCollection<ShotArrow> Arrows {
            get {
                return m_Arrows;
            }
            set {
                m_Arrows = value;
                ItemsSource = m_Arrows;
            }
        }

        public ShotArrowListView ()
        {
            ItemTemplate = new DataTemplate (typeof (PracticeArrowCell));
            PracticeArrowCell.DeletePracticeArrowClicked += ArrowDeleted;
            RowHeight = 30;

            m_Arrows = new ObservableCollection<ShotArrow> ();
            ItemsSource = m_Arrows;
        }

        public void ClearList ()
        {
            m_Arrows.Clear ();
        }

        public void ArrowDeleted (int arrowNumber)
        {
            SelectedItem = null;

            int arrayItem = -1;

            for (int i = 0; i < m_Arrows.Count; i++) {
                ShotArrow arrow = m_Arrows [i];
                if (arrow.ArrowNumber == arrowNumber) {
                    arrayItem = i;
                    break;
                }
            }

            if (arrayItem >= 0) {
                m_Arrows.RemoveAt (arrayItem);
            }

            this.Focus ();
        }

        public void Dispose ()
        {
            PracticeArrowCell.DeletePracticeArrowClicked -= ArrowDeleted;
        }
    }
}

