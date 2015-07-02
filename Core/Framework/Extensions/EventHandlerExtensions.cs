using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class EventHandlerExtensions
    {
        public static void SafeInvoke<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }

        public static void SafeInvokePropertyChanged(this PropertyChangedEventHandler eventHandler, object sender, string propertyName)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
