namespace Rybird.Framework
{
    public interface IPlatformProviders
    {
        INavigationProvider Navigation { get; }
        ISynchronizationProvider Synchronization { get; }
        IResourcesProvider Resources { get; }
    }
}
