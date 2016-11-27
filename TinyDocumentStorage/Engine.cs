using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyDocumentStorage
{
    public class TinyDocumentStorage
    {
        //establish connection to async methods
        internal Asyncs _asyncs = new Asyncs();

        public TinyDocumentStorage() { }
        public TinyDocumentStorage(string name) { this.Open_storage(name); }

        public bool Open_storage(string name)
        {
            //TO-DO
            return true;
        }

        //
        //C.R.U.D. - add / query / update / delete / commit
        //

        public bool Add(Document document)
        {
            Task<bool> task_add = _asyncs._add(document);
            task_add.Wait();
            return task_add.Result;
        }

        public List<Document> Query(string query)
        {
            Task<List<Document>> task_query = _asyncs._query(query);
            task_query.Wait();
            return task_query.Result;
        }

        public bool Update(Document old_document, Document new_document)
        {
            Task<bool> task_update = _asyncs._update(old_document,new_document);
            task_update.Wait();
            return task_update.Result;
        }

        public bool Delete(Document document)
        {
            Task<bool> task_delete = _asyncs._delete(document);
            task_delete.Wait();
            return task_delete.Result;
        }

        public bool Commit()
        {
            Task<bool> task_commit = _asyncs._commit();
            task_commit.Wait();
            return task_commit.Result;
        }

    }

    //
    // DOCUMENT
    //

    public class Document
    {
        internal long created = 0;
        internal long changed = 0;
        internal long index_page_pos = 0;
        internal long document_pos = 0;
        internal int document_length = 0;

        internal List<string> lst_tag_name = new List<string>();
        internal List<dynamic> lst_tag_value = new List<dynamic>();

        public Document() { }
        public Document(string tag, dynamic value)
        { this.Add(tag, value); }
        public void Add(string tag, dynamic value)
        {
            if (this.lst_tag_name.BinarySearch(tag) == 0)
            { this.lst_tag_name.Add(tag); this.lst_tag_value.Add(value); }
        }
    }

}
