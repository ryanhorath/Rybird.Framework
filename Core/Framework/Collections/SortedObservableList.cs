using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class SortedObservableList<T> : ObservableList<T>
    {
        private IComparer<T> _comparer;

        public SortedObservableList()
            : base()
        {

        }

        public SortedObservableList(IEnumerable<T> collection)
            : this(collection, null)
        {

        }

        public SortedObservableList(IEnumerable<T> collection, IComparer<T> comparer)
            : base()
        {
            _comparer = comparer;
            var sorted = collection.ToList();
            if (comparer == null)
            {
                sorted.Sort();
            }
            else
            {
                sorted.Sort(comparer);
            }
            foreach (var item in sorted)
            {
                base.Add(item);
            }
        }

        public new void Add(T item)
        {
            AddNearEnd(item);
        }

        public void AddNearBeginning(T item)
        {
            int index = Count;
            for (int i = 0; i < Count; i++)
            {
                if (Compare(item, this[i]) <= 0)
                {
                    index = i;
                    break;
                }
            }
            base.InsertItem(index, item);
        }

        public void AddNearEnd(T item)
        {
            int index = 0;
            for (int i = (Count - 1); i > 0; i--)
            {
                if (Compare(item, this[i]) >= 0)
                {
                    index = i + 1;
                    break;
                }
            }
            base.InsertItem(index, item);
        }

        private int Compare(T item1, T item2)
        {
            if (_comparer != null)
            {
                return _comparer.Compare(item1, item2);
            }
            else if (item1 is IComparable<T>)
            {
                return ((IComparable<T>)item1).CompareTo(item2);
            }
            else if (item1 is IComparable)
            {
                return ((IComparable)item1).CompareTo(item2);
            }
            else
            {
                throw new ArgumentException("SortedObservableList item must implement IComparable<T>, IComparable, or you must pass an IComparer<T> to the constructor.", "item1");
            }
        }
    }
}
