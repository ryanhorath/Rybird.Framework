using System;

namespace Rybird.Framework
{
    public class PlatformProviders : IPlatformProviders
    {
        public PlatformProviders(IPerAppPlatformProviders appProviders, IPerWindowPlatformProviders windowProviders)
        {
            Guard.AgainstNull(appProviders, "appProviders");
            _appProviders = appProviders;
            Guard.AgainstNull(windowProviders, "windowProviders");
            _windowProviders = windowProviders;
        }

        private readonly IPerAppPlatformProviders _appProviders;
        public IPerAppPlatformProviders AppProviders { get { return _appProviders; } }

        private readonly IPerWindowPlatformProviders _windowProviders;
        public IPerWindowPlatformProviders WindowProviders { get { return _windowProviders; } }
    }
}
