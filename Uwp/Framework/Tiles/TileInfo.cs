//using System;
//using Windows.UI.Popups;

//namespace Rybird.Framework
//{
//    public class TileInfo
//    {
//        public TileInfo(
//            string tileId,
//            string shortName,
//            string displayName,
//            Windows.UI.StartScreen.TileOptions tileOptions,
//            Uri logoUri,
//            Windows.UI.Xaml.FrameworkElement anchorElement,
//            Placement requestPlacement,
//            string arguments = null)
//        {
//            this.TileId = tileId;
//            this.ShortName = shortName;
//            this.DisplayName = displayName;
//            this.Arguments = arguments;
//            this.TileOptions = tileOptions;
//            this.LogoUri = logoUri;

//            this.AnchorElement = anchorElement;
//            this.RequestPlacement = requestPlacement;

//            this.Arguments = arguments;
//        }

//        public TileInfo(
//            string tileId,
//            string shortName,
//            string displayName,
//            Windows.UI.StartScreen.TileOptions tileOptions,
//            Uri logoUri,
//            Uri wideLogoUri,
//            Windows.UI.Xaml.FrameworkElement anchorElement,
//            Placement requestPlacement,
//            string arguments = null)
//        {
//            this.TileId = tileId;
//            this.ShortName = shortName;
//            this.DisplayName = displayName;
//            this.Arguments = arguments;
//            this.TileOptions = tileOptions;
//            this.LogoUri = logoUri;
//            this.WideLogoUri = wideLogoUri;

//            this.AnchorElement = anchorElement;
//            this.RequestPlacement = requestPlacement;

//            this.Arguments = arguments;
//        }

//        public TileInfo(
//            string tileId,
//            Windows.UI.Xaml.FrameworkElement anchorElement,
//            Placement requestPlacement)
//        {
//            this.TileId = tileId;

//            this.AnchorElement = anchorElement;
//            this.RequestPlacement = requestPlacement;
//        }

//        public string TileId { get; set; }
//        public string ShortName { get; set; }
//        public string DisplayName { get; set; }
//        public string Arguments { get; set; }
//        public Uri LogoUri { get; set; }
//        public Uri WideLogoUri { get; set; }

//        public string AppName { get; set; }
//        public int? Count { get; set; }

//        public Windows.UI.StartScreen.TileOptions TileOptions { get; set; }
//        public Windows.UI.Xaml.FrameworkElement AnchorElement { get; set; }
//        public Placement RequestPlacement { get; set; }
//    }
//}
