using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public class ObservableList<T> : ObservableCollection<T>, IReadOnlyObservableList<T>, IObservableList<T>
    {

    }
}
