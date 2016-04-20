namespace Rybird.Framework
{
    public interface IPlatformProviders
    {
        IPerAppPlatformProviders AppProviders { get; }
        IPerWindowPlatformProviders WindowProviders { get; }
    }
}
