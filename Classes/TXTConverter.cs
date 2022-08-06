using ETL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Classes
{
    internal class TxtWorker
    {
        FolderReader folderReader = new FolderReader();
        public string ReadFile(string path)
        {
            using (StreamReader streamReader = new(path))
            {
                return streamReader.ReadToEndAsync().Result;
            }
        }

        public IEnumerable<T> ConvertToObject<T>(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Type obj = typeof(T);
            
            return null;
        }
    }
}
