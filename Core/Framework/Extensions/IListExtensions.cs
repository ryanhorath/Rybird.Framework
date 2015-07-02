using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace System.Collections.Generic
{
    public static class IListExtensions
    {
        public static bool InsertUnique<T>(this IList<T> list, int index, T item)
        {
            if (list.Contains(item) == false)
            {
                list.Insert(index, item);
                return true;
            }
            return false;
        }

        public static void RemoveAll<T>(this IList<T> list)
        {
            for (int i = (list.Count - 1); i >= 0; i--)
            {
                list.RemoveAt(i);
            }
        }
    }
}
