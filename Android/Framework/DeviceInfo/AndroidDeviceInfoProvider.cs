using Android.App;
using Android.Content;
using Android.Util;

namespace Rybird.Framework
{
    public class AndroidDeviceInfoProvider : BindableBase, IDeviceInfoProvider
    {
        private readonly Activity _mainActivity;

        public AndroidDeviceInfoProvider(Activity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public string ApplicationId
        {
            get { return ""; }
        }

        public double WindowWidthInInches
        {
            get
            {
                DisplayMetrics displaymetrics = new DisplayMetrics();
                Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();.WindowManager.DefaultDisplay.GetRealMetrics(displaymetrics);
                return displaymetrics.WidthPixels / displaymetrics.Xdpi;
            }
        }

        public double WindowHeightInInches
        {
            get
            {
                DisplayMetrics displaymetrics = new DisplayMetrics();
                _mainActivity.WindowManager.DefaultDisplay.GetRealMetrics(displaymetrics);
                return displaymetrics.HeightPixels / displaymetrics.Ydpi;
            }
        }
    }
}