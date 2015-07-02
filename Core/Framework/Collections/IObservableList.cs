using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IObservableList<T> : INotifyCollectionChanged, INotifyPropertyChanged, IList<T>
    {
    }
}
