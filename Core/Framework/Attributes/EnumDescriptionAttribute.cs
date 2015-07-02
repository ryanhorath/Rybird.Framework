using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Diagnostics;

namespace Rybird.Framework
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        private static IResourcesProvider _resourcesProvider = null;
        public static void SetResourcesProvider(IResourcesProvider resourcesProvider)
        {
            _resourcesProvider = resourcesProvider;
        }

        public EnumDescriptionAttribute(string descriptionResourceName)
        {
            _descriptionResourceName = descriptionResourceName;
        }

        private string _descriptionResourceName;
        public string DescriptionResourceName
        {
            get { return _descriptionResourceName; }
        }

        public string Description
        {
            get
            {
                if (_resourcesProvider == null || string.IsNullOrEmpty(_descriptionResourceName))
                {
                    if (_resourcesProvider == null)
                    {
                        Debug.WriteLine("Warning: No IResourceProvider set for EnumDescriptionAttribute. Descriptions will not be localized.");
                    }
                    return _descriptionResourceName;
                }
                else
                {
                    return _resourcesProvider.GetString(_descriptionResourceName);
                }
            }
        }
    }
}
