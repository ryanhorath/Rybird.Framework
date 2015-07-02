using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace Rybird.Framework
{
    public class UpdateTextBindingOnPropertyChanged : DependencyObject, IBehavior
    {
        public void Attach(DependencyObject obj)
        {
            AssociatedObject = obj;
            ((TextBox)AssociatedObject).TextChanged += AttachedTextBoxTextChanged;
        }

        public void Detach()
        {
            ((TextBox)AssociatedObject).TextChanged -= AttachedTextBoxTextChanged;
        }

        #region UpdateSourceText
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(UpdateTextBindingOnPropertyChanged), new PropertyMetadata(""));

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }
        #endregion

        private static void AttachedTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                var tb = (TextBox)sender;
                tb.SetValue(UpdateTextBindingOnPropertyChanged.TextProperty, tb.Text);
            }
        }

        public DependencyObject AssociatedObject { get; private set; }
    }
}
