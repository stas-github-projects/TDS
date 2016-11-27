using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TinyDocumentStorage
{
    internal class Asyncs
    {
        internal async Task<bool> _add(Document document)
        {
            bool bool_ret = false;

            int i = 0, icount = document.lst_tag_name.Count;
            ulong u_tag_hash = 0, u_tag_value_hash = 0;
            long l_time = DateTime.Now.Ticks;
            byte b_type;
            byte[] b_data;

            //index & document
            Globals.InternalDocument _doc = new Globals.InternalDocument();
            Globals.InternalIndex _ind = new Globals.InternalIndex();

            //set tags
            for (i = 0; i < icount; i++)
            {
                b_type = Globals._dataserializer.returnTypeAndRawByteArray(document.lst_tag_value[i], out b_data);
                u_tag_hash = Globals._hash.CreateHash64bit(Encoding.ASCII.GetBytes(document.lst_tag_name[i]));
                u_tag_value_hash = Globals._hash.CreateHash64bit(b_data);
                //index
                _ind.AddIndexTag(b_type, u_tag_hash, u_tag_value_hash);
                //document
                _doc.AddDocumentTag(b_type, document.lst_tag_name[i], u_tag_hash, ref b_data);
            }//for

            //set headers: index & document
            _ind.AddHeader(Globals.storage_document_id, _ind.GetFullLength(),_doc.GetFullLength());
            _doc.AddHeader(Globals.storage_document_id, l_time, l_time, _doc.GetFullLength());

            //update globals
            Globals.storage_document_id++;

            return await Task.FromResult(bool_ret);
        }

        internal async Task<List<Document>> _query(string query)
        {
            List<Document> lst_ret = new List<Document>();



            return await Task.FromResult(lst_ret);
        }

        internal async Task<bool> _update(Document old_document, Document new_document)
        {
            bool bool_ret = false;



            return await Task.FromResult(bool_ret);
        }

        internal async Task<bool> _delete(Document document)
        {
            bool bool_ret = false;



            return await Task.FromResult(bool_ret);
        }

        internal async Task<bool> _commit()
        {
            bool bool_ret = false;



            return await Task.FromResult(bool_ret);
        }
    }
}
