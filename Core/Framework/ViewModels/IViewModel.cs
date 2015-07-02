using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rybird.Framework
{
    public interface IViewModel
    {
        ICommand SaveCommand { get; }
    }
}
