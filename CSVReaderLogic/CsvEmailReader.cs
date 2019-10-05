using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using CSVReaderInterface;

namespace CSVReaderLogic
{
    public class CsvEmailReader<T> : IDataReader<T>
    {
        public List<T> ReadFile(string path, int count, int skip)
        {
            using (var csv = new CsvReader(new StreamReader(path)))
            {
                return csv.GetRecords<T>().Skip(skip).Take(count).ToList();
            }
        }
    }
}