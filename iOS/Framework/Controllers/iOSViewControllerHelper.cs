using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Rybird.Framework.iOS.Controllers
{
    public class iOSViewControllerHelper
    {
        private IiOSViewController _controller;
        private bool _isNew = true;
        private string _parameter;

        public iOSViewControllerHelper(IiOSViewController controller, string parameter)
        {
            _controller = controller;
            _parameter = parameter;
        }

        public void ViewDidLoad()
        {
            // Only call Create() once - in low memory, iOS may destroy the View, but not the ViewController, so this method will be called again
            // even though the ViewModel/ViewController is not new.
            if (_isNew)
            {
                _isNew = false;
                _controller.ViewModel.Create(_parameter);
            }
        }

        public void ViewWillAppear()
        {
            _controller.ViewModel.Activate();
        }

        public void ViewWillDisappear()
        {
            _controller.ViewModel.Deactivate();
        }

        public void EncodeRestorableState(NSCoder coder)
        {
            _controller.ViewModel.SaveState(new NSCoderFacade(coder));
        }

        public void DecodeRestorableState(NSCoder coder)
        {
            _controller.ViewModel.LoadState(new NSCoderFacade(coder));
        }
    }
}