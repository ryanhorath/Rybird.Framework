using System.ComponentModel;

namespace Rybird.Framework
{
    public interface IDeviceInfoProvider : INotifyPropertyChanged
    {
        string ApplicationId { get; }
        double WindowWidthInInches { get; }
        double WindowHeightInInches { get; }
    }
}