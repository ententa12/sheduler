using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Sheduler.Model;

namespace Sheduler.EmailReader
{
    static class CsvEmailReader<T>
    {
        public static List<T> ReadCsv()
        {
            using (var csv = new CsvReader(new StreamReader("C:\\csv\\EmailList.csv")))
            {
                return csv.GetRecords<T>().ToList();
            }
        }
    }
}
