using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailDatabaseInterface
{
    public interface IDatabaseContext<T>
    {
        void Save(T obj);
        bool CheckIfExist(T obj);
        int HigherIndex();
        Task Dispose();
    }
}