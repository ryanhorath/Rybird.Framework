using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IMvvmTypeResolver
    {
        Type ResolveViewTypeFromViewModelType(Type viewModelType);
        Type ResolveViewModelTypeFromViewType(Type viewType);
        FrameworkPageViewModel InstantiatePageViewModel(Type viewModelType, IPlatformProviders providers);
        IFrameworkPage InstantiatePageView(Type pageViewType);
    }
}
