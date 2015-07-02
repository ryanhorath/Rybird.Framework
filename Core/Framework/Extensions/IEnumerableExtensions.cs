using Rybird.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        public static IObservableList<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var observableCollection = new ObservableList<T>();
            foreach (var item in source)
            {
                observableCollection.Add(item);
            }
            return observableCollection;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null || !source.Any());
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        {
            return !source.IsNullOrEmpty();
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            foreach (T item in collection)
            {
                action(item);
            }
        }
    }
}
