using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

namespace Rybird.Framework
{
    public interface ICollectionViewModel<T> : INotifyPropertyChanged
    {
        ObservableCollection<T> Collection { get; }
        T SelectedItem { get; set; }
        DelegateCommand AddCommand { get; }
        DelegateCommand RemoveCommand { get; }
        bool IsEmpty { get; }
        event EventHandler<CollectionViewModelEventArgs<T>> ItemAdded;
        event EventHandler<CollectionViewModelEventArgs<T>> ItemRemoved;
    }

    public class CollectionViewModelEventArgs<T> : EventArgs
    {
        private T _item;

        public CollectionViewModelEventArgs(T item)
        {
            _item = item;
        }

        public T Item { get { return _item; } }
    }
}
