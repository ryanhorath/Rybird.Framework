using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Rybird.Framework
{
    public interface IDialogViewModel
    {
        double Width { get; }
        double Height { get; }
        string Title { get; }
        string Icon { get; }
        bool ShowInTaskbar { get; }
        bool? DialogResult { get; set; }
    }
}
