using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Rybird.Framework
{
    public class DialogViewModel<T> : ViewModelBase, IDialogViewModel where T : ViewModelBase, IDialogContentViewModel
    {
        public DialogViewModel(T content)
        {
            _content = content;
        }

        private T _content;
        public T Content
        {
            get { return _content; }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { SetProperty<double>(ref _width, value); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { SetProperty<double>(ref _height, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty<string>(ref _title, value); }
        }

        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set { SetProperty<string>(ref _icon, value); }
        }

        private bool _showInTaskbar = false;
        public bool ShowInTaskbar
        {
            get { return _showInTaskbar; }
            set { SetProperty<bool>(ref _showInTaskbar, value); }
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set { SetProperty<bool?>(ref _dialogResult, value); }
        }

        private void OnOkCommand()
        {
            Content.OnBeforeOkCommand();
            DialogResult = true;
        }

        private bool OnCanOkCommand()
        {
            return Content.CanOkCommand;
        }

        private void OnCancelCommand()
        {
            DialogResult = false;
        }

        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get { return _okCommand ?? (_okCommand = new DelegateCommand<object>(param => this.OnOkCommand(), param => this.OnCanOkCommand())); }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new DelegateCommand<object>(param => this.OnCancelCommand())); }
        }
    }
}
