using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace Rybird.Framework
{
    public class FrameworkActivity<T> : FrameworkActivity
            where T : FrameworkPageViewModel
    {
        public T ViewModel { get { return (T) DataContext; } }
    }

    public class FrameworkActivity : Activity, IFrameworkActivity
    {
        private readonly ActivityHelper _activityHelper;

        public FrameworkActivity()
        {
            _activityHelper = new ActivityHelper(this);
        }

        public void InitializeActivity(IAndroidNavigationProvider navigation, Bundle savedInstanceState)
        {
            _activityHelper.InitializeActivity(navigation, savedInstanceState);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _activityHelper.OnCreate(Intent, bundle);
        }

        protected override void OnStart()
        {
            base.OnStart();
            _activityHelper.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _activityHelper.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _activityHelper.OnPause(IsChangingConfigurations);
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            _activityHelper.OnRestart();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            _activityHelper.OnSaveInstanceState(outState, IsChangingConfigurations);
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            base.OnRestoreInstanceState(savedInstanceState);
            _activityHelper.OnRestoreInstanceState(savedInstanceState);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _activityHelper.OnDestroy(IsChangingConfigurations);
        }

        public FrameworkPageViewModel DataContext
        {
            get { return _activityHelper.DataContext; }
            set { _activityHelper.DataContext = value; }
        }

        public Activity Activity { get { return this; } }

        public string ActivityInstanceId
        {
            get { return _activityHelper.ActivityInstanceId; }
        }
    }
}
