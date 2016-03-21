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

namespace Rybird.Framework
{
    public abstract class FrameworkAppBase : Application, IFrameworkApp
    {
        protected FrameworkAppBase(Type mainPageViewModelType)
        {
            _mainPageViewModelType = mainPageViewModelType;
            this.Suspending += OnSuspending;
        }

        public static T As<T>() where T : FrameworkAppBase
        {
            return (T)Application.Current;
        }

        private Type _mainPageViewModelType;
        protected Type MainPageViewModelType { get { return _mainPageViewModelType; } }

        protected Frame RootFrame { get; private set; }

        public INavigationProvider Navigation { get; private set; }

        public bool IsSuspending { get; private set; }

        private ILoggingProvider _logger;
        public virtual ILoggingProvider Logger
        {
            get { return _logger ?? (_logger = new DefaultLoggingProvider()); }
        }

        // TODO: Figure out what to do with this
        //private IFrameworkContainer _container;
        //protected virtual IFrameworkContainer Container
        //{
        //    get { return _container ?? (_container = new DefaultFrameworkContainer(DefaultViewModelResolver)); }
        //}

        private IMvvmTypeResolver _typeResolver;
        protected virtual IMvvmTypeResolver TypeResolver
        {
            get { return _typeResolver ?? (_typeResolver = new DefaultMvvmTypeResolver()); }
        }

        private ISynchronizationProvider _synchronization;
        protected virtual ISynchronizationProvider Synchronization
        {
            get { return _synchronization ?? (_synchronization = new SynchronizationProvider()); }
        }

        private IResourcesProvider _resources;
        protected virtual IResourcesProvider ResourcesProvider
        {
            get { return _resources ?? (_resources = new ResourcesFacade(ResourceLoader.GetForViewIndependentUse(Constants.StoreAppsInfrastructureResourceMapId))); }
        }

        protected virtual INavigationProvider CreateNavigationManager(Frame frame, IMvvmTypeResolver typeResolver)
        {
            return new WindowsRuntimeNavigationProvider(this, frame, typeResolver, PlatformProviders);
        }

        private IPlatformProviders _platformProviders;
        private IPlatformProviders PlatformProviders
        {
            get { return _platformProviders ?? (_platformProviders = new PlatformProviders(Navigation, Synchronization, ResourcesProvider, null)); }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            InitializeFrameAsync(e);
            // Restore the app if necessary, otherwise navigate to main page
            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated
                && ShouldAppRestoreAfterTermination(e.Kind))
            {
                try
                {
                    await Navigation.LoadState();
                }
                catch (SessionStateServiceException)
                {
                    // Something went wrong restoring state.
                    // Assume there is no state and continue
                }
            }
            else
            {
                await Navigation.NavigateAsync(_mainPageViewModelType, e.Arguments);
            }
            Window.Current.Activate();
        }

        protected virtual void OnInitialize(IActivatedEventArgs args) { }

        private FrameworkPageViewModel DefaultViewModelResolver(Type type)
        {
            return Activator.CreateInstance(type, PlatformProviders) as FrameworkPageViewModel;
        }

        protected void InitializeFrameAsync(IActivatedEventArgs args)
        {
            if (RootFrame == null && Window.Current.Content == null)
            {
                RootFrame = new Frame();
                RootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
                Navigation = CreateNavigationManager(RootFrame, TypeResolver);
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
                await Navigation.SaveState();
                await OnSuspend();
                deferral.Complete();
            }
            finally
            {
                IsSuspending = false;
            }
        }

        protected async virtual Task OnSuspend()
        {
            await Task.Factory.StartNew(() => { });
        }
    }
}
