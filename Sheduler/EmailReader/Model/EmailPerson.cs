using System;
using System.Collections.Generic;
using System.Text;

namespace Sheduler.Model
{
    class EmailPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        override public string ToString()
        {
            return "ID: " + Id + " First Name: " + FirstName + " Last Name: " + LastName + " Title: " + Title + " Message: " + Message;
        }
    }
}
