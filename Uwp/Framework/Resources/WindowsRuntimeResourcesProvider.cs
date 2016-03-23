using Rybird.Framework;
using Windows.ApplicationModel.Resources;

namespace Rybird.Framework
{
    public class WindowsRuntimeResourcesProvider : IResourcesProvider
    {
        private readonly ResourceLoader _resourceLoader;

        public WindowsRuntimeResourcesProvider(ResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public string GetString(string resource)
        {
            return _resourceLoader.GetString(resource);
        }
    }
}
