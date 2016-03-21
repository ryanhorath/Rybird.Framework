using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rybird.Framework
{
    public class UpdatePasswordBindingOnPropertyChanged : DependencyObject, IBehavior
    {
        public void Attach(DependencyObject obj)
        {
            AssociatedObject = obj;
            ((PasswordBox)AssociatedObject).PasswordChanged += AttachedPasswordBoxPasswordChanged;
        }

        public void Detach()
        {
            ((PasswordBox)AssociatedObject).PasswordChanged -= AttachedPasswordBoxPasswordChanged;
        }

        #region UpdateSourceText
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(UpdatePasswordBindingOnPropertyChanged), new PropertyMetadata(""));

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }
        #endregion

        private static void AttachedPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox)
            {
                var pb = (PasswordBox)sender;
                pb.SetValue(UpdatePasswordBindingOnPropertyChanged.PasswordProperty, pb.Password);
            }
        }

        public DependencyObject AssociatedObject { get; private set; }
    }
}
