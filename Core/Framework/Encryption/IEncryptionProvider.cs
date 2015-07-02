using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface IEncryptionProvider
    {
        Stream EncryptStream(Stream stream);
        Stream DecryptStream(Stream stream);
    }
}
