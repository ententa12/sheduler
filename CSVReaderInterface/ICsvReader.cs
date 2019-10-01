using System.Collections.Generic;

namespace CSVReaderInterface
{
    public interface ICsvReader<T>
    {
        List<T> ReadCsv(string path, int count, int skip);
    }
}