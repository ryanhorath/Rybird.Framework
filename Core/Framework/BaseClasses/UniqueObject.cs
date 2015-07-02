using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public abstract class UniqueObject : ModelBase, IUniqueObject
    {
        public UniqueObject() : this(0)
        {
        }

        public UniqueObject(int uniqueId)
        {
            _uniqueId = uniqueId;
        }

        private int _uniqueId;
        public int UniqueId
        {
            get { return _uniqueId; }
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UniqueObject);
        }

        public bool Equals(UniqueObject obj)
        {
            if (obj != null)
            {
                if (ReferenceEquals(this, obj)
                    || obj.UniqueId == UniqueId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
