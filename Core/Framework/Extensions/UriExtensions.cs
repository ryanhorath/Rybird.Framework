using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class UriExtensions
    {
        public static IDictionary<string, string> GetQueryParameters(this Uri uri, bool mergeDuplicateKeys = false)
        {
            return uri.ToString().ParseQueryParameters();
        }
    }
}
