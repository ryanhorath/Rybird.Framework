using System;
using System.Windows;
using Windows.UI.Xaml;

namespace Rybird.Framework
{
    public class BindingChangedEventArgs : EventArgs
    {
        public BindingChangedEventArgs(DependencyPropertyChangedEventArgs e)
        {
            this.EventArgs = e;
        }

        public DependencyPropertyChangedEventArgs EventArgs { get; private set; }
    }
}