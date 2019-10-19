using System.Collections.Generic;

namespace CSVReaderInterface
{
    public interface IDataReader<T>
    {
        List<T> ReadFile(string path, int count, int skip);
    }
}