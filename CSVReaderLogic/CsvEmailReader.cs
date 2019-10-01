using CsvHelper;
using CSVReaderInterface;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVReaderLogic
{
    public class CsvEmailReader<T> : ICsvReader<T>
    {
        public List<T> ReadCsv(string path, int count, int skip)
        {
            using (var csv = new CsvReader(new StreamReader(path)))
            {
                return csv.GetRecords<T>().Skip(skip).Take(count).ToList();
            }
        }
    }
}
