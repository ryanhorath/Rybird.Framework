using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace Rybird.Framework
{
    public class MultiSelectBehavior : DependencyObject, IBehavior
    {
        private bool _selectionChangedInProgress; // Flag to avoid infinite loop if same viewmodel is shared by multiple controls

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems", typeof(IList), typeof(MultiSelectBehavior),
            new PropertyMetadata(null, SelectedItemsChangedCallback));
        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public void Attach(DependencyObject obj)
        {
            AssociatedObject = obj;
            ((ListView)AssociatedObject).SelectionChanged += OnSelectionChanged;
        }

        public void Detach()
        {
            ((ListView)AssociatedObject).SelectionChanged -= OnSelectionChanged;
        }

        public DependencyObject AssociatedObject { get; private set; }

        private static void SelectedItemsChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            NotifyCollectionChangedEventHandler handler = (s, e) => SelectedItemsChanged(sender, e);
            if (args.OldValue is INotifyCollectionChanged)
            {
                (args.OldValue as INotifyCollectionChanged).CollectionChanged -= handler;
            }
            if (args.NewValue is INotifyCollectionChanged)
            {
                (args.NewValue as INotifyCollectionChanged).CollectionChanged += handler;
            }
        }

        private static void SelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender is MultiSelectBehavior)
            {
                var listViewBase = (sender as MultiSelectBehavior).AssociatedObject;
                var listSelectedItems = ((ListView)listViewBase).SelectedItems;
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems)
                    {
                        if (listSelectedItems.Contains(item))
                        {
                            listSelectedItems.Remove(item);
                        }
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        if (!listSelectedItems.Contains(item))
                        {
                            listSelectedItems.Add(item);
                        }
                    }
                }
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = SelectedItems as IList;
            if (!_selectionChangedInProgress)
            {
                _selectionChangedInProgress = true;
                foreach (var item in e.RemovedItems)
                {
                    if (list.Contains(item))
                    {
                        list.Remove(item);
                    }
                }
            }
            foreach (var item in e.AddedItems)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            _selectionChangedInProgress = false;
        }
    }
}
