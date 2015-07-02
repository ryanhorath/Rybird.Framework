using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Rybird.Framework.iOS.Controllers
{
    public class iOSTabBarController : UITabBarController, IiOSViewController
    {
        private iOSViewControllerHelper _helper;
        private string _parameter;

        public void Initialize(FrameworkPageViewModel dataContext, string parameter)
        {
            ViewModel = dataContext;
            _parameter = parameter;
            _helper = new iOSViewControllerHelper(this, parameter);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _helper.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            _helper.ViewWillAppear();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            _helper.ViewWillDisappear();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        public override void EncodeRestorableState(NSCoder coder)
        {
            base.EncodeRestorableState(coder);
            _helper.EncodeRestorableState(coder);
        }

        public override void DecodeRestorableState(NSCoder coder)
        {
            base.DecodeRestorableState(coder);
            _helper.DecodeRestorableState(coder);
        }

        public FrameworkPageViewModel ViewModel
        {
            get;
            private set;
        }
    }
}