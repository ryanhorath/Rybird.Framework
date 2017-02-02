using Rybird.Framework;
using Windows.ApplicationModel.Resources;

namespace Rybird.Framework
{
    public class UwpResourcesProvider : IResourcesProvider
    {
        private readonly ResourceLoader _resourceLoader;

        public UwpResourcesProvider(ResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public string GetString(string resource)
        {
            return _resourceLoader.GetString(resource);
        }
    }
}
