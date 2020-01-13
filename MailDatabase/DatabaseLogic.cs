﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CSVEmailModel;
using LiteDB;
using MailDatabaseInterface;

namespace MailDatabase
{
    public class DatabaseLogic : IDatabaseContext<EmailPerson>
    {
        private readonly LiteDatabase _db;
        private readonly LiteCollection<EmailPerson> _emails;

        public DatabaseLogic()
        {
            _db = new LiteDatabase(@"Mail.db");
            _emails = _db.GetCollection<EmailPerson>();
        }

        public Task Save(EmailPerson obj)
        {
            return new Task(() =>
            {
                _emails.Insert(obj);
                _db.Commit();
            });
        }

        public Task SaveAll(IEnumerable<EmailPerson> obj)
        {
            return new Task(() =>
            {
                _emails.Insert(obj);
                _db.Commit();
            });
        }

        public bool CheckIfExist(EmailPerson obj)
        {
            return _emails.Exists(email => email.Id == obj.Id);
        }

        public int LastIndex()
        {
            return _emails.Max();
        }

        public Task Dispose()
        {
            return new Task(() => _db.Dispose());
        }
    }
}