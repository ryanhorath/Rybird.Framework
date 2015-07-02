using Rybird.Framework;
using System.Windows;

namespace Rybird.Framework
{
    public class WpfResourcesProvider : IResourcesProvider
    {  
        public string GetString(string resource)
        {
            return (string)Application.Current.FindResource(resource);
        }
    }
}
