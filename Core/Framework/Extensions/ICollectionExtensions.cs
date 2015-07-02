using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace System.Collections.Generic
{
    public static class ICollectionExtensions
    {
        public static bool ContainsIndex<T>(this ICollection<T> collection, int index)
        {
            return collection.IsNullOrEmpty<T>() ? false : index < collection.Count;
        }

        public static bool AddUnique<T>(this ICollection<T> collection, T value)
        {
            bool alreadyHas = collection.Contains(value);
            if (!alreadyHas)
            {
                collection.Add(value);
            }
            return alreadyHas;
        }

        public static int AddRangeUnique<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            int count = 0;
            foreach (T value in values)
            {
                if (collection.AddUnique(value))
                {
                    count++;
                }
            }
            return count;
        }

        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> items)
        {
            if (list is List<T>)
            {
                ((List<T>)list).AddRange(items);
            }
            else
            {
                foreach (T item in items)
                {
                    list.Add(item);
                }
            }
        }

        public static void MergeAndRemoveMissingFrom<T>(this ICollection<T> list, IEnumerable<T> sourceList)
        {
            var removed = list.Except(sourceList);
            foreach (var item in removed)
            {
                list.Remove(item);
            }
            var added = sourceList.Except(list);
            foreach (var item in added)
            {
                list.Add(item);
            }
        }
    }
}
