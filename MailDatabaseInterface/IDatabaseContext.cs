using System.Collections.Generic;

namespace MailDatabaseInterface
{
    public interface IDatabaseContext<T>
    {
        void Save(T obj);
        bool CheckIfExist(T obj);
        int HigherIndex();
        List<T> GetElements();
    }
}