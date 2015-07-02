using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface ISimpleDialogProvider
    {
        Task<bool> ShowDialogAsync(string title, string message, string confirmText, string cancelText = null);
    }
}
