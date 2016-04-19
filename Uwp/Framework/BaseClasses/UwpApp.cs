using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Rybird.Framework;
using Windows.ApplicationModel.DataTransfer;
using Nito.AsyncEx;

namespace Rybird.Framework
{
    public abstract class WindowsRuntimeApp : Application, IFrameworkApp
    {
        protected WindowsRuntimeApp(Type mainPageViewModelType)
        {
            _mainPageViewModelType = mainPageViewModelType;
            Suspending += OnSuspending;
            Resuming += OnResuming;
        }

        private Type _mainPageViewModelType;
        protected Type MainPageViewModelType { get { return _mainPageViewModelType; } }

        protected Frame RootFrame { get; private set; }

        public bool IsSuspending { get; private set; }

        private INavigationProvider _navigationProvider;
        protected virtual INavigationProvider NavigationProvider
        {
            get { return _navigationProvider; }
        }
        private ILoggingProvider _loggingProvider;
        private IMvvmTypeResolver _typeResolver;
        private ISynchronizationProvider _synchronization;
        private IResourcesProvider _resources;
        private ILifecycleProvider _lifecycleProvider;

        protected virtual INavigationProvider CreateNavigationManager(Window window, IMvvmTypeResolver typeResolver, 
            ISynchronizationProvider synchronizationProvider, IResourcesProvider resourcesProvider)
        {
            return new UwpNavigationProvider(window, typeResolver, synchronizationProvider, resourcesProvider);
        }

        // Called only when app is launched through the app's main tile
        // In this case OnActivated will not be called
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            InitializeFrameAsync(args);
            // Restore the app if necessary, otherwise navigate to main page
            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated
                && ShouldAppRestoreAfterTermination(args.Kind))
            {
                try
                {
                    await _lifecycleProvider.LoadState();
                }
                catch (SessionStateServiceException)
                {
                    // Something went wrong restoring state.
                    // Assume there is no state and continue
                }
            }
            else
            {
                await NavigationProvider.NavigateAsync(_mainPageViewModelType, args.Arguments);
            }
            Window.Current.Activate();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            InitializeFrameAsync(args);
        }

        protected virtual void OnInitialize(IActivatedEventArgs args)
        {
            _loggingProvider = new DefaultLoggingProvider();
            _synchronization = new SynchronizationProvider();
            _resources = new WindowsRuntimeResourcesProvider(ResourceLoader.GetForViewIndependentUse(Constants.StoreAppsInfrastructureResourceMapId));
            _typeResolver = new DefaultMvvmTypeResolver();
            _lifecycleProvider = new UwpLifecycleProvider(_typeResolver, _synchronization, _resources);
            _navigationProvider = CreateNavigationManager(Window.Current, _typeResolver, _synchronization, _resources);
        }

        private FrameworkPageViewModel DefaultViewModelResolver(Type type)
        {
            return Activator.CreateInstance(type, new PlatformProviders(_navigationProvider, _synchronization, _resources)) as FrameworkPageViewModel;
        }

        protected void InitializeFrameAsync(IActivatedEventArgs args)
        {
            if (RootFrame == null && Window.Current.Content == null)
            {
                RootFrame = new Frame();
                RootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
                OnInitialize(args);

                Window.Current.Content = RootFrame;
            }
            else
            {
                if (Window.Current.Content != null)
                {
                    RootFrame = Window.Current.Content as Frame;
                }
            }
            Window.Current.Activate();
        }

        protected virtual bool ShouldAppRestoreAfterTermination(ActivationKind kind)
        {
            // Only restore state if the app is launched normally
            return kind == ActivationKind.Launch;
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            IsSuspending = true;
            try
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                await _lifecycleProvider.SaveState();
                await OnSuspend();
                deferral.Complete();
            }
            finally
            {
                IsSuspending = false;
            }
        }

        protected virtual Task OnSuspend()
        {
            return TaskConstants.Completed;
        }

        // Resume is called only if the app is already in memory but has been suspended - ie it is not being "launched"
        // Of note, if the user invokes the app through any means other than just going back to it (share, open file, etc),
        // then the app will Launch and not Resume (you can launch an app that is already running)
        private void OnResuming(object sender, object e)
        {
            // When app is suspended and resumed, OnNavigatedFrom() has been called, but
            // OnNavigatedTo() will not be called on Resume. This code gives the Page a
            // chance to call ViewModel.Activate()
            IWindowsRuntimeFrameworkPage page = RootFrame != null ? RootFrame.Content as IWindowsRuntimeFrameworkPage : null;
            if (page != null)
            {
                page.OnAppResuming();
            }
            // TODO: Create a deferral object using AsyncEx and pass the deferral object through here, allowing this method to be run asynchronously
            var result = OnResume();
        }

        protected virtual Task OnResume()
        {
            return TaskConstants.Completed;
        }
    }
}
