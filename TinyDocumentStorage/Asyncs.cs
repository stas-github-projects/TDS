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
