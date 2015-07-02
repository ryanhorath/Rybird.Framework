using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Rybird.Framework
{
    public class FrameworkPage : ContentPage, IXamarinFormsFrameworkPage
    {
        private bool _isNew = true;
        private string _parameter;

        public FrameworkPageViewModel ViewModel
        {
            get
            {
                return (FrameworkPageViewModel)BindingContext;
            }
        }

        public void Initialize(FrameworkPageViewModel viewModel, string parameter)
        {
            BindingContext = viewModel;
            _parameter = parameter;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                if (_isNew)
                {
                    ViewModel.Create(_parameter);
                    _isNew = false;
                }
                // TODO: Fix actual state saving
                ViewModel.LoadState(new NullStateBucket());
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (ViewModel != null)
            {
                // TODO: Fix actual state saving
                ViewModel.SaveState(new NullStateBucket());
            }
        }

        public Page Page
        {
            get { return this; }
        }
    }
}
