using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface INavigationProvider
    {
        Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel;
        bool CanOpenWindow { get; }
        Task<bool> NavigateAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel;
        Task<bool> NavigateAsync(Type viewModelType, string parameter = null);
        bool CanGoBack { get; }
        Task<bool> GoBackAsync();
        Task LoadState();
        Task SaveState();
    }
}
