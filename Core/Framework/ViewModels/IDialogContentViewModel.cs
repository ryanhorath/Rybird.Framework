using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Rybird.Framework
{
    public interface IDialogContentViewModel
    {
        bool CanOkCommand { get; }
        void OnBeforeOkCommand();
    }
}
