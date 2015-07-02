using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Windows.Input;

namespace Rybird.Framework
{
    internal class ButtonBinding<T> : ViewBinding
    {
        private Button _button;
        private ICommand _targetCommand;
        private Func<T> _parameterCallback;

        public ButtonBinding(Button button, ICommand targetCommand) : this(button, targetCommand, null)
        {

        }

        public ButtonBinding(Button button, ICommand targetCommand, Func<T> parameterCallback) : base(button)
        {
            _button = button;
            _button.Click += new EventHandler(HandleClick);
            _targetCommand = targetCommand;
            _parameterCallback = parameterCallback;
        }

        private void HandleClick(object sender, EventArgs e)
        {
            object parameter = null;
            if (_parameterCallback != null)
            {
                parameter = _parameterCallback();
            }
            if (_targetCommand.CanExecute(parameter))
            {
                _targetCommand.Execute(parameter);
            }
        }

        protected override void Cleanup()
        {
            _button.Click -= new EventHandler(HandleClick);
        }
    }
}