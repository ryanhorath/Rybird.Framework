using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System;
using Rybird.Framework;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public abstract class FrameworkPageViewModel : FrameworkViewModel, IViewModel
    {
        public const string NavigationParameterKey = "RybirdFramework-NavigationParameter";
        protected INavigationProvider Navigation { get; private set; }
        protected ISynchronizationProvider Synchronization { get; private set; }
        protected IResourcesProvider Resources { get; private set; }
        private bool _pageLoaded = false;
        private bool _stateLoaded = false;

        public FrameworkPageViewModel(IPlatformProviders arguments)
        {
            Navigation = arguments.WindowProviders.Navigation;
            Synchronization = arguments.WindowProviders.Synchronization;
            Resources = arguments.WindowProviders.Resources;
        }

        private void SetIsNew()
        {
            if (_pageLoaded && _stateLoaded)
            {
                IsNew = false;
            }
        }

        public void Activate()
        {
            OnActivated();
            _pageLoaded = true;
            SetIsNew();
        }

        public void Deactivate()
        {
            OnDeactivated();
        }

        public void LoadState(IReadOnlyStateBucket state)
        {
            RestoreRestorableState(state);
            OnLoadState(state);
            _stateLoaded = true;
            SetIsNew();
        }

        public void SaveState(IStateBucket state)
        {
            SaveRestorableState(state);
            OnSaveState(state);
        }

        public void Create(string parameter)
        {
            OnCreated(parameter);
        }

        protected virtual void OnCreated(string parameter) { }
        protected virtual void OnActivated() { }
        protected virtual void OnDeactivated() { }
        protected virtual void OnLoadState(IReadOnlyStateBucket sessionState) { }
        protected virtual void OnSaveState(IStateBucket sessionState) { }

        private void SaveRestorableState(IStateBucket state)
        {
            var viewModelProperties = GetType().GetRuntimeProperties().Where(c => c.GetCustomAttribute(typeof(RestorableStateAttribute)) != null);
            foreach (PropertyInfo propertyInfo in viewModelProperties)
            {
                state[propertyInfo.Name] = propertyInfo.GetValue(this);
            }
        }

        private void RestoreRestorableState(IReadOnlyStateBucket state)
        {
            var viewModelProperties = GetType().GetRuntimeProperties().Where(c => c.GetCustomAttribute(typeof(RestorableStateAttribute)) != null);
            foreach (PropertyInfo propertyInfo in viewModelProperties)
            {
                if (state.ContainsKey(propertyInfo.Name))
                {
                    propertyInfo.SetValue(this, state[propertyInfo.Name]);
                }
            }
        }

        public virtual string PageTitle { get { return ""; } }

        public void NavigateBack()
        {
            Navigation.GoBackAsync();
        }

        public bool CanGoBack
        {
            get { return Navigation.CanGoBack; }
        }

        protected bool IsNew { get; private set; }

        #region Commands
        #region SaveCommand
        private DelegateCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new DelegateCommand(() => SaveCommandExecute(), () => SaveCommandCanExecute()));
            }
        }

        private void SaveCommandExecute()
        {
            if (OnSaveCommandCanExecute())
            {
                OnSave();
            }
            else
            {
                OnSaveFailure();
            }
        }

        private bool SaveCommandCanExecute()
        {
            return OnSaveCommandCanExecute();
        }

        protected virtual bool OnSaveCommandCanExecute()
        {
            return !HasErrors;
        }

        protected virtual void OnSave()
        {

        }

        protected virtual void OnSaveFailure()
        {

        }
        #endregion
        #region BackCommand
        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get { return _backCommand ?? (_backCommand = new DelegateCommand(() => BackCommandExecute(), () => BackCommandCanExecute())); }
        }

        private void BackCommandExecute()
        {
            Navigation.GoBackAsync();
        }

        private bool BackCommandCanExecute()
        {
            return Navigation.CanGoBack;
        }
        #endregion
        #endregion
    }
}
