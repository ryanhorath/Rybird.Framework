using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Rybird.Framework
{
    public static class ItemsControlExtensions
    {
        public static T FindItemsPanel<T>(this ItemsControl itemsControl) where T : Panel
        {
            return FindItemsPanel<T>(itemsControl, itemsControl);
        }

        private static T FindItemsPanel<T>(ItemsControl root, DependencyObject dependencyObject) where T : Panel
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i) as DependencyObject;
                if (child != null)
                {
                    if (child is T && ReferenceEquals(ItemsControl.GetItemsOwner(child), root))
                    {
                        object temp = child;
                        return (T)temp;
                    }
                    T panel = FindItemsPanel<T>(root, child);
                    if (panel != null)
                    {
                        object temp = panel;
                        return (T)temp;
                    }
                }
            }
            return default(T);
        }
    }
}
