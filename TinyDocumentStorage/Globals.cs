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

        internal static List<InternalIndex> lst_index_to_save = new List<InternalIndex>();
        internal static List<InternalDocument> lst_docs_to_save = new List<InternalDocument>();
        internal class InternalDocument
        {
            internal byte active;
            internal byte data_type;

            internal InternalDocument() { }
            internal InternalDocument (byte[] input) //fill internaldocument with existing byte array' data
            { 
                
            }

            internal byte[] ToBytes()
            {
                return null; 
            }
        }

        ///<summary>
        ///header = active - [1], doc_id - [8], index_len - [4]
        ///<para>tags = active - [1], data_type - [1], tag_name_hash - [8], tag_value_hash - [8]</para>
        ///</summary>
        internal class InternalIndex
        {
            internal byte[] index_data;
        }

        internal static class DocumentPreferences
        {
            internal static int tag_name_length = 20;
            internal static ulong document_id = 0;
        }

    }
}
