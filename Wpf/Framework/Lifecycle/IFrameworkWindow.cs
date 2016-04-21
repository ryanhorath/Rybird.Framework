using System.Windows.Navigation;
using System.Windows.Threading;

namespace Rybird.Framework
{
    public interface IFrameworkWindow : IGloballyUniqueObject
    {
        Dispatcher Dispatcher { get ; }
        NavigationService NavigationService { get; }
    }
}
