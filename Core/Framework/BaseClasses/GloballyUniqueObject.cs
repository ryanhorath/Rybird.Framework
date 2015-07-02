using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public abstract class GloballyUniqueObject : ViewModelBase, IGloballyUniqueObject
    {
        public GloballyUniqueObject() : this(Guid.Empty)
        {
        }

        public GloballyUniqueObject(Guid uniqueId)
        {
            _uniqueId = uniqueId;
        }

        private Guid _uniqueId = Guid.Empty;
        public Guid UniqueId
        {
            get { return _uniqueId; }
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GloballyUniqueObject);
        }

        public bool Equals(GloballyUniqueObject obj)
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
