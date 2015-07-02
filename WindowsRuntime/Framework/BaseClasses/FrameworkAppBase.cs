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

        private IFrameworkContainer _container;
        protected virtual IFrameworkContainer Container
        {
            get { return _container ?? (_container = new DefaultFrameworkContainer(DefaultViewModelResolver)); }
        }

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

        protected virtual INavigationProvider CreateNavigationManager(Frame frame, IFrameworkContainer container, IMvvmTypeResolver typeResolver)
        {
            return new WindowsRuntimeNavigationProvider(this, frame, container, typeResolver);
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
                    await Navigation.RestoreSavedState();
                }
                catch (SessionStateServiceException)
                {
                    // Something went wrong restoring state.
                    // Assume there is no state and continue
                }
            }
            else
            {
                Navigation.Navigate(_mainPageViewModelType, args.Arguments);
            }
            Window.Current.Activate();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            InitializeFrameAsync(args);
        }

        protected virtual void OnInitialize(IActivatedEventArgs args) { }

        private FrameworkPageViewModel DefaultViewModelResolver(Type type)
        {
            return Activator.CreateInstance(type, new DefaultFrameworkPageViewModelArguments(Navigation, Synchronization, ResourcesProvider)) as FrameworkPageViewModel;
        }

        protected void InitializeFrameAsync(IActivatedEventArgs args)
        {
            if (RootFrame == null && Window.Current.Content == null)
            {
                RootFrame = new Frame();
                RootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];
                Navigation = CreateNavigationManager(RootFrame, Container, TypeResolver);
                Container.RegisterInstance<INavigationProvider>(Navigation);

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
                await Navigation.Suspend();
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
