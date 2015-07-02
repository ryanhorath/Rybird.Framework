using Android.App;
using Android.Content;
using Android.OS;

namespace Rybird.Framework
{
    public interface IFrameworkActivity : ILayoutInflaterProvider
    {
        FrameworkPageViewModel DataContext { get; set; }
        void StartActivity(Intent intent);
        Activity Activity { get; }
        ActionBar ActionBar { get; }
        void InitializeActivity(IAndroidNavigationProvider navigation, Bundle savedInstanceState);
        string ActivityInstanceId { get; }
    }
}
