using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Rybird.Framework
{
    public class MetroListBoxViewModel<T> : ViewModelBase
    {
        private Action _addCallback;
        private Action<T> _removeCallback;
        private int _minimumItems = 1;

        public MetroListBoxViewModel(ObservableCollection<T> collection, Action addCallback, Action<T> removeCallback, string title, string emptyCollectionMessage, int minimumItems)
            : this(collection, addCallback, removeCallback, title, emptyCollectionMessage, true, true, minimumItems)
        {

        }

        public MetroListBoxViewModel(ObservableCollection<T> collection, Action addCallback, Action<T> removeCallback, string title, string emptyCollectionMessage) : this(collection, addCallback, removeCallback, title, emptyCollectionMessage, true, true, 1)
        {
            
        }

        public MetroListBoxViewModel(ObservableCollection<T> collection, Action addCallback, Action<T> removeCallback, string title, string emptyCollectionMessage, bool showAdd, bool showRemove, int minimumItems)
        {
            _collection = collection;
            _collection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChanged);
            _addCallback = addCallback;
            _removeCallback = removeCallback;
            _title = title;
            _emptyCollectionMessage = emptyCollectionMessage;
            _showAdd = showAdd;
            _showRemove = showRemove;
            _minimumItems = minimumItems;
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

        private ObservableCollection<T> _collection;
        public ObservableCollection<T> Collection
        {
            get { return _collection; }
        }

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new DelegateCommand(() => this.OnAddCommand(), () => this.OnCanAddCommand())); }
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

        private bool _canAdd = true;
        public bool CanAdd
        {
            get { return _canAdd; }
            private set { SetProperty<bool>(ref _canAdd, value); }
        }

        private bool _canRemove = true;
        public bool CanRemove
        {
            get { return _canRemove; }
            private set { SetProperty<bool>(ref _canRemove, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty<string>(ref _title, value); }
        }

        private string _emptyCollectionMessage;
        public string EmptyCollectionMessage
        {
            get { return _emptyCollectionMessage; }
            set { SetProperty<string>(ref _emptyCollectionMessage, value); }
        }

        private bool _showAdd;
        public bool ShowAdd
        {
            get { return _showAdd; }
            set { SetProperty<bool>(ref _showAdd, value); }
        }

        private bool _showRemove;
        public bool ShowRemove
        {
            get { return _showRemove; }
            set { SetProperty<bool>(ref _showRemove, value); }
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
            _addCallback();
        }

        private bool OnCanAddCommand()
        {
            return CanAdd;
        }

        public void OnRemoveCommand()
        {
            if (SelectedItem != null)
            {
                var item = SelectedItem;
                SelectedItem = default(T);
                _removeCallback(item);
            }
        }

        private bool OnCanRemoveCommand()
        {
            if (CanRemove && Collection.Count > _minimumItems && SelectedItem != null)
            {
                return true;
            }
            return false;
        }
    }
}
