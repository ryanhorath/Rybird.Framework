using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Rybird.Framework
{
    public class CollectionViewModel<T> : ViewModelBase, ICollectionViewModel<T>
    {
        private Func<T> _addFunction;

        public event EventHandler<CollectionViewModelEventArgs<T>> ItemAdded;
        public event EventHandler<CollectionViewModelEventArgs<T>> ItemRemoved;

        public CollectionViewModel(ObservableCollection<T> collection, Func<T> addFunction)
        {
            _collection = collection;
            _collection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
            _addFunction = addFunction;
            SetIsEmpty();
        }

        private void CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetIsEmpty();
        }

        private void SetIsEmpty()
        {
            IsEmpty = Collection == null || Collection.Count == 0;
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new DelegateCommand(() => this.OnAddCommand())); }
        }

        private DelegateCommand _removeCommand;
        public DelegateCommand RemoveCommand
        {
            get { return _removeCommand ?? (_removeCommand = new DelegateCommand(() => this.OnRemoveCommand(), () => this.OnCanRemoveCommand())); }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            private set { SetProperty<bool>(ref _isEmpty, value); }
        }

        private ObservableCollection<T> _collection;
        public ObservableCollection<T> Collection
        {
            get { return _collection; }
        }

        private T _selectedItem;
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty<T>(ref _selectedItem, value); RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        public void OnAddCommand()
        {
            T newItem = _addFunction();
            if (newItem != null)
            {
                Collection.Add(newItem);
                RaiseItemAddedEvent(newItem);
            }
        }

        public void OnRemoveCommand()
        {
            if (SelectedItem != null)
            {
                T item = SelectedItem;
                SelectedItem = default(T);
                Collection.Remove(item);
                RaiseItemRemovedEvent(item);
            }
        }

        private bool OnCanRemoveCommand()
        {
            if (SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void RaiseItemAddedEvent(T item)
        {
            var itemAdded = ItemAdded;
            if (itemAdded != null)
            {
                itemAdded(this, new CollectionViewModelEventArgs<T>(item));
            }
        }

        private void RaiseItemRemovedEvent(T item)
        {
            var itemRemoved = ItemRemoved;
            if (itemRemoved != null)
            {
                itemRemoved(this, new CollectionViewModelEventArgs<T>(item));
            }
        }
    }
}
