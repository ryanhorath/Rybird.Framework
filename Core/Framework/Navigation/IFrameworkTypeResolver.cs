using System;

namespace Rybird.Framework
{
    public interface IFrameworkTypeResolver
    {
        Type ResolveViewTypeFromViewModelType(Type viewModelType);
        Type ResolveViewModelTypeFromViewType(Type viewType);
        FrameworkPageViewModel InstantiatePageViewModel(Type viewModelType, IPlatformProviders providers);
        IFrameworkPage InstantiatePageView(Type pageViewType);
        IPlatformProviders GetProvidersForWindow(object window);
    }
}
