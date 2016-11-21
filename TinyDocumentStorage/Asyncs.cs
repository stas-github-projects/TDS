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
            byte b_type;
            byte[] b_data;

            for (i = 0; i < icount; i++)
            {
                b_type = Globals._dataserializer.returnTypeAndRawByteArray(document.lst_tag_value[i], out b_data);
                
                Globals.InternalDocument _int = new Globals.InternalDocument();

            }//for
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
