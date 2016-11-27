using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using TinyDocumentStorage_test;

namespace TinyDocumentStorage_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sss = new Stopwatch();
            sss.Start();

            TinyDocumentStorage.TinyDocumentStorage _storage = new TinyDocumentStorage.TinyDocumentStorage();


            for (int i = 0; i < 10; i++)
            {
                TinyDocumentStorage.Document _doc = new TinyDocumentStorage.Document();
                _doc.Add("tag", "toys");
                _doc.Add("manufacturer", "Toy Inc Co");
                _doc.Add("name", "plane fx-" + i);
                _doc.Add("color", "white");
                _doc.Add("price", 1299);
                _storage.Add(_doc);

                TinyDocumentStorage.Document _doc2 = new TinyDocumentStorage.Document();
                _doc2.Add("tag", "gadjets");
                _doc2.Add("manufacturer", "Apple");
                _doc2.Add("name", "iphone 7 cables");
                _doc2.Add("color", "black");
                _doc2.Add("price", i * 12 / 3.14);
                _storage.Add(_doc2);
            }

            //_storage.Commit();
            //_storage.Query("");


            sss.Stop();
            Console.WriteLine("\nElapsed time: {0} ticks / {1} msecs",sss.ElapsedTicks,sss.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
