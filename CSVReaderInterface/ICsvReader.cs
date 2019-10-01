using System;
using System.Collections.Generic;
using System.Text;

namespace CSVReaderInterface
{
    public interface ICsvReader<T>
    {
        List<T> ReadCsv(string path, int count, int skip);
    }
}
