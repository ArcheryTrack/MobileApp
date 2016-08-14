using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ATMobile.Controls
{
    public abstract class AbstractLoadMoreListView<T> : AbstractListView where T : new()
    {
        protected ObservableCollection<T> Rows;

        public AbstractLoadMoreListView ()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;

            Rows = new ObservableCollection<T> ();
            ItemsSource = Rows;
        }

        public abstract void LoadMoreData (int start);

        void InfiniteListView_ItemAppearing (object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;

            if (items != null) {
                int start = items.Count;

                if (e.Item == items [start - 1]) {
                    LoadMoreData (start);
                }
            }
        }

        public void AppendRows (List<T> _newRows)
        {
            foreach (var item in _newRows) {
                Rows.Add (item);
            }
        }
    }
}

