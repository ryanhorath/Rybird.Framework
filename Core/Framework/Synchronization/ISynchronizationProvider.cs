using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface ISynchronizationProvider
    {
        Task RunAsync(Action action);
    }
}