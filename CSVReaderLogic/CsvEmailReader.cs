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
            using (var streamReader = new StreamReader(path))
            {
                using (var csv = new CsvReader(streamReader))
                {
                    return csv.GetRecords<T>().Skip(skip).Take(count).ToList();
                }
            }
        }
    }
}