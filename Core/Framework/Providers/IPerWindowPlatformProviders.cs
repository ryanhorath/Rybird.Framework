using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IPerWindowPlatformProviders
    {
        INavigationProvider Navigation { get; }
        ISynchronizationProvider Synchronization { get; }
        IResourcesProvider Resources { get; }
    }
}
