using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Reflection;
using Nito.AsyncEx;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace Rybird.Framework
{
    public class FrameworkWindow : Window, IGloballyUniqueObject
    {
        private readonly Guid _uniqueId = Guid.NewGuid();
        public Guid UniqueId { get { return _uniqueId; } }

        private readonly Dispatcher _dispatcher;
        public Dispatcher Dispatcher { get { return _dispatcher; } }
    }
}
