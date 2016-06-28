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
    public class PracticeArrowListView : AbstractListView, IDisposable
    {
        private ObservableCollection<ShotArrow> m_Arrows;

        public DeletePracticeArrowClickedDelegate DeletePracticeArrowClicked;
        public List<ShotArrow> Arrows {
            get {
                return m_Arrows.ToList ();
            }
            set {
                m_Arrows.Clear ();

                foreach (var item in value) {
                    m_Arrows.Add (item);
                }
            }
        }


        public PracticeArrowListView ()
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
            var deleteClicked = DeletePracticeArrowClicked;
            if (deleteClicked != null) {
                deleteClicked (arrowNumber);
            }

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
        }

        public void Dispose ()
        {
            PracticeArrowCell.DeletePracticeArrowClicked -= ArrowDeleted;
        }
    }
}

