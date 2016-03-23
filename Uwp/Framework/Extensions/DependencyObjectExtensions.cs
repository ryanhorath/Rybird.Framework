using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Rybird.Framework
{
    public static class DependencyObjectExtensions
    {
        public static T FindVisualParent<T>(this DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }
            else
            {
                T parent = parentObject as T;
                return parent ?? FindVisualParent<T>(parentObject);
            }
        }

        public static T FindVisualChildByName<T>(this DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }
            T foundChild = null;
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                var childType = child as T;
                if (childType == null)
                {
                    foundChild = FindVisualChildByName<T>(child, childName);
                    if (foundChild != null)
                    {
                        break;
                    }
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        public static IList<T> GetVisualChildrenOfType<T>(this DependencyObject parent) where T : DependencyObject
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildrenOfType<T>(parent, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildrenOfType<T>(DependencyObject parent, IList<T> visualCollection) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildrenOfType(child, visualCollection);
                }
            }
        }
    }
}
