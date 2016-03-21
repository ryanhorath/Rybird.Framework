using System;

namespace Rybird.Framework
{
    public interface IFlyoutViewModel
    {
        Action GoBack { get; set; }
        Action CloseFlyout { get; set; }
        void Open(object parameter, Action successAction);
    }
}
