using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface ISessionStateService
    {
        void RegisterKnownType(Type type);
        Task SaveAsync();
        Task RestoreSessionStateAsync();
        void RestoreFrameState();
        IDictionary<string, object> GetPageSessionState();
        void RemovePageSessionState();
    }
}
