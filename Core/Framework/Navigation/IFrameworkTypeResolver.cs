using System;
using System.Collections.Generic;
using System.Linq;
namespace Rybird.Framework
{
    public interface IFrameworkTypeResolver
    {
        Type ResolveViewTypeFromViewModelType(Type viewModelType);
        Type ResolveViewModelTypeFromViewType(Type viewType);
        FrameworkPageViewModel InstantiatePageViewModel(Type viewModelType, IPlatformProviders providers);
        IFrameworkPage InstantiatePageView(Type pageViewType);
        IPerWindowPlatformProviders GetProvidersForWindow(object window);
    }
}
