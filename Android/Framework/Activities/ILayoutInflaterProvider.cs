using Android.Views;

namespace Rybird.Framework
{
    public interface ILayoutInflaterProvider
    {
        LayoutInflater LayoutInflater { get; }
    }
}