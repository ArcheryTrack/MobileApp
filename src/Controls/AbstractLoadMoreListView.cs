using System;
using System.Collections;
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
        }

        public abstract void LoadMoreData (IList items);

        void InfiniteListView_ItemAppearing (object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;

            if (items != null
                && e.Item == items [items.Count - 1]) {
                LoadMoreData (items);
            }
        }
    }
}

