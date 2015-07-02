using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public static class Guard
    {
        public static void AgainstNull<T>(T obj, string propertyName) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(propertyName);
            }
        }
    }
}
