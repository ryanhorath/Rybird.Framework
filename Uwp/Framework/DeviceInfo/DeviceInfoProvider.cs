using System.Xml.Linq;
using Windows.Graphics.Display;
using Windows.UI.Xaml;

namespace Rybird.Framework
{
    public class DeviceInfoProvider : BindableBase, IDeviceInfoProvider
    {
        private static readonly XDocument manifest = XDocument.Load("AppxManifest.xml", LoadOptions.None);
        private static readonly XNamespace xNamespace = XNamespace.Get("http://schemas.microsoft.com/appx/2010/manifest");

        public DeviceInfoProvider()
        {
            Window.Current.SizeChanged += Current_SizeChanged;
            CalculateWindowSizeInInches();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            CalculateWindowSizeInInches();
        }

        private void CalculateWindowSizeInInches()
        {
            WindowWidthInInches = Window.Current.Bounds.Width / DisplayInformation.GetForCurrentView().LogicalDpi;
            WindowHeightInInches = Window.Current.Bounds.Height / DisplayInformation.GetForCurrentView().LogicalDpi;
        }

        public string ApplicationId
        {
            get
            {
                // Get the Application node located under Package/Applications
                var applications = manifest.Descendants(xNamespace + "Application");
                foreach (var application in applications)
                {
                    if (application.Attribute("Id") != null)
                    {
                        return application.Attribute("Id").Value;
                    }
                }
                return string.Empty;
            }
        }

        private double _windowWidthInInches;
        public double WindowWidthInInches
        {
            get { return _windowWidthInInches; }
            private set { SetProperty<double>(ref _windowWidthInInches, value); }
        }

        private double _windowHeightInInches;
        public double WindowHeightInInches
        {
            get { return _windowHeightInInches; }
            private set { SetProperty<double>(ref _windowHeightInInches, value); }
        }
    }
}