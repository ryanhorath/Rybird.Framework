using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Rybird.Framework
{
    public class VisualStatesProvider : IVisualStatesProvider
    {
        public VisualStatesProvider(Frame frame)
        {
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            _widthInInches = null;
            UpdateVisualState();
        }

        private double? _widthInInches = null;
        private double WidthInInches
        {
            get
            {
                if (!_widthInInches.HasValue)
                {
                    _widthInInches = Window.Current.Bounds.Width / DisplayInformation.GetForCurrentView().LogicalDpi;
                }
                return _widthInInches.Value;
            }
        }

        private void UpdateVisualState()
        {
            var width = WidthInInches;
            var frame = (Frame)Window.Current.Content;
            var page = frame != null ? frame.Content : null;
            if (page != null)
            {
                if (width <= 6.5)
                {
                    GoToState(page, "Small", true);
                }
                else if (width <= 20.0)
                {
                    GoToState(page, "Normal", true);
                }
                else
                {
                    GoToState(page, "Large", true);
                }
            }
        }

        public bool GoToState(object target, string stateName, bool useAnimations)
        {
            return VisualStateManager.GoToState((Control)target, stateName, useAnimations);
        }
    }
}
