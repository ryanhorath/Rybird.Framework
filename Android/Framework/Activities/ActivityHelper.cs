using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.ComponentModel;
using System.Windows.Input;

namespace Rybird.Framework
{
    public class ActivityHelper
    {
        public const string ACTIVITY_ID_KEY = "Rybird.Framework.ActivityId";
        private IFrameworkActivity _activity;
        private readonly IList<Action<Result, Intent>> _resultHandlers = new List<Action<Result, Intent>>();
        private string _parameter = null;
        private bool _ignoreLifecycleEventsForCachedViewModel = false;

        public ActivityHelper(IFrameworkActivity activity)
        {
            _activity = activity;
        }

        public void InitializeActivity(IAndroidNavigationProvider navigation, Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
            {
                _activityInstanceId = savedInstanceState.GetString(ACTIVITY_ID_KEY);
            }
            else
            {
                _activityInstanceId = Guid.NewGuid().ToString();
            }
            var isViewModelCached = navigation.InitializeActivity(_activity);
            _ignoreLifecycleEventsForCachedViewModel = isViewModelCached;
        }

        public void OnCreate(Intent intent, Bundle bundle)
        {
            if (!_ignoreLifecycleEventsForCachedViewModel)
            {
                if (intent.Extras != null && intent.Extras.ContainsKey(FrameworkPageViewModel.NavigationParameterKey))
                {
                    _parameter = intent.Extras.Get(FrameworkPageViewModel.NavigationParameterKey).ToString();
                } 
                _activity.DataContext.Create(_parameter);

            }
            if (_activity.ActionBar != null)
            {
                //AddBinding(new PropertyBinding<string, string>(_activity.ActionBar, "Title", _activity.DataContext, "PageTitle"));
            }
        }

        public void OnStart()
        {

        }

        public void OnResume()
        {
            if (!_ignoreLifecycleEventsForCachedViewModel)
            {
                _activity.DataContext.Activate();
            }
            _ignoreLifecycleEventsForCachedViewModel = false;
        }

        public void OnPause(bool isChangingConfigurations)
        {
            if (!isChangingConfigurations)
            {
                _activity.DataContext.Deactivate();
            }
        }

        public void OnRestart()
        {

        }

        public void OnRestoreInstanceState(Bundle bundle)
        {
            if (!_ignoreLifecycleEventsForCachedViewModel && bundle != null)
            {
                _activity.DataContext.LoadState(new BundleFacade(bundle));
            }
        }

        public void OnSaveInstanceState(Bundle bundle, bool isChangingConfigurations)
        {
            bundle.PutString(ACTIVITY_ID_KEY, _activityInstanceId);
            if (!isChangingConfigurations)
            {
                _activity.DataContext.SaveState(new BundleFacade(bundle));
            }
        }

        public void OnDestroy(bool isChangingConfigurations)
        {

        }

        private string _activityInstanceId;
        public string ActivityInstanceId
        {
            get { return _activityInstanceId; }
        }

        private FrameworkPageViewModel _dataContext;
        public FrameworkPageViewModel DataContext
        {
            get
            {
                if (_dataContext == null)
                {
                    throw new Exception("The DataContext is null. Use the INavigationService navigation methods to ensure that the DataContext is set correctly.");
                }
                return _dataContext;
            }
            set
            {
                value.ThrowIfNull("value");
                _dataContext = value;
            }
        }

        private IFrameworkApp App
        {
            get
            {
                return null; // IFrameworkApp.As<IFrameworkApp>();
            }
        }
    }
}