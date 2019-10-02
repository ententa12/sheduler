using System.Collections.Generic;
using System.Linq;
using CSVEmailModel;
using LiteDB;
using MailDatabaseInterface;

namespace MailDatabase
{
    public class DatabaseLogic : IDatabaseContext<EmailPerson>
    {
        private LiteDatabase _db;
        private LiteCollection<EmailPerson> _emails;

        public DatabaseLogic()
        {
            _db = new LiteDatabase(@"Mail.db");
            _emails = _db.GetCollection<EmailPerson>();
        }

        public void Save(EmailPerson obj)
        {
            _emails.Insert(obj);
        }

        public bool CheckIfExist(EmailPerson obj)
        {
            return _emails.Exists(email => email.Id == obj.Id);
        }

        public int HigherIndex()
        {
            return _emails.Max();
        }

        public List<EmailPerson> GetElements()
        {
            return _emails.FindAll().ToList();
        }

        public LiteCollection<EmailPerson> Emails => _emails;
    }
}