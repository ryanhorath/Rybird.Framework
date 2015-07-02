using Rybird.Framework;
using Windows.ApplicationModel.Resources;

namespace Rybird.Framework
{
    public class ResourcesFacade : IResourcesProvider
    {
        private readonly ResourceLoader _resourceLoader;

        public ResourcesFacade(ResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public string GetString(string resource)
        {
            return _resourceLoader.GetString(resource);
        }
    }
}
