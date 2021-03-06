﻿using Android.Content;
using System;

namespace Rybird.Framework
{
    public class AndroidFrameworkTypeResolver : FrameworkTypeResolverBase
    {
        private readonly Guid _mainWindowGuid = Guid.NewGuid();
        private readonly Context _applicationContext;

        public AndroidFrameworkTypeResolver(Context applicationContext)
        {
            Guard.AgainstNull(applicationContext, "applicationContext");
            _applicationContext = applicationContext;
        }

        protected override IPlatformProviders GeneratePerWindowProviders(object window)
        {
            var resourcesProvider = new AndroidResourcesProvider(_applicationContext);
            var synchronizationProvider = new AndroidSynchronizationProvider();
            var navigationProvider = new AndroidNavigationProvider(this, synchronizationProvider, resourcesProvider);
            var providers = new PlatformProviders(navigationProvider, synchronizationProvider, resourcesProvider);
            return providers;
        }

        protected override Guid GetUniqueIdForWindow(object window)
        {
            return _mainWindowGuid;
        }
    }
}
