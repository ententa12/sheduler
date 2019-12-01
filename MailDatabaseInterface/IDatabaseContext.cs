using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailDatabaseInterface
{
    public interface IDatabaseContext<T>
    {
        Task Save(T obj);
        Task SaveAll(IEnumerable<T> obj);
        bool CheckIfExist(T obj);
        int LastIndex();
        Task Dispose();
    }
}