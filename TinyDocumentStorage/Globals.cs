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

        //header [3], version [1], first_index_page_pos [8], last_index_page_pos [8], last_index_page_freecell [2]
        internal static int storage_header_max_len = 4 + 8 + 8 + 2;
        internal static int storage_document_tag_name_len = 20;

        //active = 1, index_doc_id = 8, index_len = 4, index_doc_len = 4
        internal static int storage_index_header_len = 1 + 8 + 4 + 4;
        //active [1], data_type [1], tag_hash [8], tag_value_hash [8]
        internal static int storage_index_tag_block_len = 1 + 1 + 8 + 8;

        //doc_id = 1, doc_id = 8, doc_created = 8, doc_changed = 8, doc_length = 4
        internal static int storage_document_header_len = 1 + 8 + 8 + 8 + 4;
        //active [1], data_type [1], tag_name [x], tag_hash [8], tag_value_hash [8]
        internal static int storage_document_tag_block_len = 1 + 1 + storage_document_tag_name_len + 8 + 8;


        internal static List<InternalIndex> lst_index_to_save = new List<InternalIndex>();
        internal static List<InternalDocument> lst_docs_to_save = new List<InternalDocument>();

        ///<summary>
        ///header = active - [1], doc_id - [8], created - [8], changed - [8], document_len - [8]
        ///<para>tags = active - [1], data_type - [1], tag_name - [X], tag_name_hash - [8], tag_value_hash - [8]</para>
        ///</summary>
        internal class InternalDocument
        {
            internal byte[] document_header;
            internal List<byte[]> lst_document_tag = new List<byte[]>();

            internal void AddHeader(ulong doc_id, long created, long changed, int doc_len)
            {
                int ipos = 0;
                document_header = new byte[Globals.storage_document_header_len];
                _service.InsertBytes(ref document_header, (byte)1, ipos); ipos++;
                _service.InsertBytes(ref document_header, BitConverter.GetBytes(doc_id), ipos); ipos += 8; //doc_id
                _service.InsertBytes(ref document_header, BitConverter.GetBytes(created), ipos); ipos += 8; //created
                _service.InsertBytes(ref document_header, BitConverter.GetBytes(changed), ipos); ipos += 8; //changed
                _service.InsertBytes(ref document_header, BitConverter.GetBytes(doc_len), ipos); ipos += 4; //doc_len
            }
            //active [1], data_type [1], tag_name [x], tag_hash [8], tag_value_hash [8]
            internal void AddDocumentTag (byte data_type, string tag_name, ulong tag_hash, ulong tag_value_hash)
            {
                int ipos = 0;
                byte[] b_tag_block = new byte[Globals.storage_document_tag_block_len];
                _service.InsertBytes(ref b_tag_block, (byte)1, ipos); ipos++; //active
                _service.InsertBytes(ref b_tag_block, data_type, ipos); ipos++; //data_type
                _service.InsertBytes(ref b_tag_block, Encoding.ASCII.GetBytes(tag_name), ipos); ipos += Globals.storage_document_tag_name_len; //tag_name
                _service.InsertBytes(ref b_tag_block, BitConverter.GetBytes(tag_hash), ipos); ipos += 8; //tag_hash
                _service.InsertBytes(ref b_tag_block, BitConverter.GetBytes(tag_value_hash), ipos); ipos += 8; //tag_value_hash
                lst_document_tag.Add(b_tag_block); //add to list
            }

            internal byte[] ToBytes() //merge header & tags
            {
                int i = 0, icount = lst_document_tag.Count, ipos = 0, ilen = storage_document_header_len + (icount * Globals.storage_document_tag_block_len);

                byte[] b_buffer = new byte[storage_document_header_len]; //return
                _service.InsertBytes(ref b_buffer, this.document_header, ipos); ipos += storage_document_header_len; //add header
                for (i = 0; i < icount; i++)
                {
                    _service.InsertBytes(ref b_buffer, this.lst_document_tag[i], ipos); ipos += Globals.storage_document_tag_block_len;
                }
                return b_buffer; 
            }
        }

        ///<summary>
        ///header = active - [1], doc_id - [8], index_len - [4]
        ///<para>tags = active - [1], data_type - [1], tag_name_hash - [8], tag_value_hash - [8]</para>
        ///</summary>
        internal class InternalIndex
        {        //active = 1, index_doc_id = 8, index_len = 4, index_doc_len = 4
            internal byte[] index_header;
            internal List<byte[]> lst_index_tag = new List<byte[]>();
            internal void AddHeader(ulong doc_id, int index_len, int index_doc_len)
            {
                int ipos = 0;
                index_header = new byte[Globals.storage_index_header_len];
                _service.InsertBytes(ref index_header, (byte)1, ipos); ipos++;
                _service.InsertBytes(ref index_header, BitConverter.GetBytes(doc_id), ipos); ipos += 8; //doc_id
                _service.InsertBytes(ref index_header, BitConverter.GetBytes(index_len), ipos); ipos += 4; //index_len
                _service.InsertBytes(ref index_header, BitConverter.GetBytes(index_doc_len), ipos); ipos += 4; //index_doc_len
            }

            internal void AddIndexTag(byte data_type, ulong tag_hash, ulong tag_value_hash)
            {
                int ipos = 0;
                byte[] b_tag_block = new byte[Globals.storage_document_tag_block_len];
                _service.InsertBytes(ref b_tag_block, (byte)1, ipos); ipos++; //active
                _service.InsertBytes(ref b_tag_block, data_type, ipos); ipos++; //data_type
                _service.InsertBytes(ref b_tag_block, BitConverter.GetBytes(tag_hash), ipos); ipos += 8; //tag_hash
                _service.InsertBytes(ref b_tag_block, BitConverter.GetBytes(tag_value_hash), ipos); ipos += 8; //tag_value_hash
                lst_index_tag.Add(b_tag_block); //add to list
            }
            internal byte[] ToBytes() //merge header & tags
            {
                int i = 0, icount = lst_index_tag.Count, ipos = 0, ilen = Globals.storage_index_header_len + (icount * Globals.storage_index_tag_block_len);

                byte[] b_buffer = new byte[storage_index_header_len]; //return
                _service.InsertBytes(ref b_buffer, this.index_header, ipos); ipos += storage_document_header_len; //add header
                for (i = 0; i < icount; i++)
                {
                    _service.InsertBytes(ref b_buffer, this.lst_index_tag[i], ipos); ipos += Globals.storage_index_tag_block_len;
                }
                return b_buffer;
            }
        }

        internal static class DocumentPreferences
        {
            internal static int tag_name_length = 20;
            internal static ulong document_id = 0;
        }

    }
}
