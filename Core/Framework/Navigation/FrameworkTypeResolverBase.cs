using System;
using System.Collections.Generic;

namespace Rybird.Framework
{
    public abstract class FrameworkTypeResolverBase : IFrameworkTypeResolver
    {
        private IDictionary<Guid, IPlatformProviders> _providersCache = new Dictionary<Guid, IPlatformProviders>();

        public virtual Type ResolveViewTypeFromViewModelType(Type viewModelType)
        {
            var viewName = viewModelType
                .AssemblyQualifiedName
                .Replace(viewModelType.Name, viewModelType.Name.Replace("PageViewModel", "PageView"))
                .Replace(".ViewModels", ".Views");
            return Type.GetType(viewName);
        }

        public virtual Type ResolveViewModelTypeFromViewType(Type viewType)
        {
            var viewModelName = viewType
                .AssemblyQualifiedName
                .Replace(viewType.Name, viewType.Name.Replace("PageView", "PageViewModel"))
                .Replace(".ViewModels", ".Views");
            return Type.GetType(viewModelName);
        }

        public virtual FrameworkPageViewModel InstantiatePageViewModel(Type viewModelType, IPlatformProviders providers)
        {
            return Activator.CreateInstance(viewModelType, providers) as FrameworkPageViewModel;
        }

        public virtual IFrameworkPage InstantiatePageView(Type type)
        {
            return (IFrameworkPage)Activator.CreateInstance(type);
        }

        public virtual IPlatformProviders GetProvidersForWindow(object window)
        {
            var uniqueId = GetUniqueIdForWindow(window);
            return _providersCache.GetValueOrAddDefault(uniqueId, () => GeneratePerWindowProviders(window));
        }

        protected abstract IPlatformProviders GeneratePerWindowProviders(object window);
        protected abstract Guid GetUniqueIdForWindow(object window);
    }
}
