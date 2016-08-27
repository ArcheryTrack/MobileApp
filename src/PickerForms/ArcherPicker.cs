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
        private List<Guid> m_Excluded;

        public ArcherPicker (List<Guid> _excluded = null) : base ("Pick Archer", "Archer")
        {
            m_Excluded = _excluded;
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

            foreach (var item in temp) {
                if (!m_Excluded.Contains (item.Id)) {
                    final.Add (item);
                }
            }

            List.ItemsSource = final;
        }
    }
}

