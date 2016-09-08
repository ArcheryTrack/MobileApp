using System;
using System.Collections.Generic;
using ATMobile.Cells;
using ATMobile.Forms;
using ATMobile.Managers;
using ATMobile.Objects;

namespace ATMobile.PickerForms
{
    public class ArcherPicker : AbstractGenericPicker<Archer, ArcherCell>
    {
        private List<Guid> m_Subset;
        private bool m_ExclusionSubset;

        public ArcherPicker (List<Guid> _subset = null,
                             bool _exclusionSubset = true)
            : base ("Pick Archer", "Archer")
        {
            m_Subset = _subset;
            m_ExclusionSubset = _exclusionSubset;
        }

        public override void AddPressed ()
        {
            ArcherForm addArcher = new ArcherForm ();
            Navigation.PushModalAsync (addArcher);
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            List<Archer> temp = ATManager.GetInstance ().GetArchers ();

            List<Archer> final = new List<Archer> ();

            if (m_Subset != null) {
                if (m_ExclusionSubset) {
                    //We are removing items based on subset
                    foreach (var item in temp) {
                        if (!m_Subset.Contains (item.Id)) {
                            final.Add (item);
                        }
                    }
                } else {
                    //We are adding items based on subset
                    foreach (var item in temp) {
                        if (m_Subset.Contains (item.Id)) {
                            final.Add (item);
                        }
                    }
                }
            } else {
                final = temp;
            }

            List.ItemsSource = final;
        }
    }
}

