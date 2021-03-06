﻿using System;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface INavigationProvider
    {
        Task<bool> NavigateAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel;
        Task<bool> NavigateAsync(Type viewModelType, string parameter = null);
        bool CanGoBack { get; }
        Task<bool> GoBackAsync();
        // TODO: Add this for supporting tabbed interfaces, etc.
        //IPlatformProviders GenerateProvidersForChildFrame();
        Task OpenWindowAsync<TViewModel>(string parameter = null) where TViewModel : FrameworkPageViewModel;
        bool CanOpenWindow { get; }
        Task LoadState();
        Task SaveState();
    }
}
