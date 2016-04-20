namespace Rybird.Framework
{
    public class PerAppPlatformProviders : IPerAppPlatformProviders
    {
        public PerAppPlatformProviders(ILifecycleProvider lifecycle)
        {
            Guard.AgainstNull(lifecycle, "lifecycle");
            _lifecycle = lifecycle;
        }

        private readonly ILifecycleProvider _lifecycle;
        public ILifecycleProvider Lifecycle { get { return _lifecycle; } }
    }
}
