using System;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Rybird.Framework
{
    public class FrameworkWindow : NavigationWindow, IGloballyUniqueObject
    {
        private readonly Guid _uniqueId = Guid.NewGuid();
        public Guid UniqueId { get { return _uniqueId; } }

        private readonly Dispatcher _dispatcher;
        public Dispatcher Dispatcher { get { return _dispatcher; } }
    }
}
