namespace Rybird.Framework
{
    public interface ICapabilitiesFacade
    {
        string DeviceName { get; }
        string DeviceManufaturer { get; }
        long DeviceTotalMemory { get; }
        bool IsSearchEnabled { get; }
        string ApplicationId { get; }
    }
}