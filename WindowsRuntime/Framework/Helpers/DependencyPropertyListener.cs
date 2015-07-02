using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Rybird.Framework
{
    public class DependencyPropertyListener
    {
        private readonly DependencyProperty property;
        private static int index;
        private FrameworkElement target;

        public DependencyPropertyListener()
        {
            this.property =
                DependencyProperty.RegisterAttached(
                    "DependencyPropertyListener" + index++,
                    typeof(object),
                    typeof(DependencyPropertyListener),
                    new PropertyMetadata(null, this.HandleValueChanged));
        }

        public event EventHandler<BindingChangedEventArgs> Changed;

        public void Attach(FrameworkElement element, Binding binding)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (this.target != null)
            {
                throw new InvalidOperationException("Cannot attach an already attached listener");
            }

            this.target = element;

            this.target.SetBinding(this.property, binding);
        }

        public void Detach()
        {
            this.target.ClearValue(this.property);
            this.target = null;
        }

        private void HandleValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Changed != null)
            {
                this.Changed(this, new BindingChangedEventArgs(e));
            }
        }
    }
}
