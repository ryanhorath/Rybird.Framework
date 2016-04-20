using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Reflection;
using Nito.AsyncEx;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using System.Threading;
using System.Windows.Threading;

namespace Rybird.Framework
{
    public interface IFrameworkWindow : IGloballyUniqueObject
    {
        Dispatcher Dispatcher { get ; }
    }
}
