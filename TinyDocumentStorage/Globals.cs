using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinyDocumentStorage
{
    internal static class Globals
    {
        internal static Service _service = new Service();
        internal static HashFNV _hash = new HashFNV();
        internal static DataTypeSerializer _dataserializer = new DataTypeSerializer();
    }
}
