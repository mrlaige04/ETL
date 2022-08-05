using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using ETL.Classes;
using ETL.Interfaces;

namespace ETL.Classes
{
    internal class CSVReader 
    {
        StreamReader sreader;
        CsvReader csvreader;
        
        public CSVReader(string path)
        {
            sreader = new(path);
            csvreader = new(sreader, culture: CultureInfo.InvariantCulture);
        }
        public IEnumerable<T> GetAllRecords<T>()
        {
            return csvreader.GetRecords<T>();
        }
    }
}
