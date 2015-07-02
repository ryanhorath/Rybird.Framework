using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class DefaultMvvmTypeResolver : IMvvmTypeResolver
    {
        public Type ResolveViewTypeFromViewModelType(Type viewModelType)
        {
            var viewName = viewModelType
                .AssemblyQualifiedName
                .Replace(viewModelType.Name, viewModelType.Name.Replace("PageViewModel", "PageView"))
                .Replace(".ViewModels", ".Views");
            return Type.GetType(viewName);
        }

        public Type ResolveViewModelTypeFromViewType(Type viewType)
        {
            var viewModelName = viewType
                .AssemblyQualifiedName
                .Replace(viewType.Name, viewType.Name.Replace("PageView", "PageViewModel"))
                .Replace(".ViewModels", ".Views");
            return Type.GetType(viewModelName);
        }

        public FrameworkPageViewModel InstantiatePageViewModel(Type viewModelType, IPlatformProviders providers)
        {
            return Activator.CreateInstance(viewModelType, providers) as FrameworkPageViewModel;
        }

        public IFrameworkPage InstantiatePageView(Type type)
        {
            return (IFrameworkPage)Activator.CreateInstance(type);
        }
    }
}
