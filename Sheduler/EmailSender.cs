using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GemBox.Email;
using GemBox.Email.Smtp;
using Sheduler.Model;

namespace Sheduler
{
    public static class EmailSender
    {

        const string Host = "smtp.gmail.com";
        const string Username = "scheduler.ztp";
        const string Password = "ZAQ!2wsx";
        const string Sender = "scheduler.ztp@gmail.com";

        static int SentEmailCounter = 0;

        public static void SendEmail(EmailPerson emailPerson)
        {
            // If using Professional version, put your serial key below.
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var mailingList = 
                emailPerson.Email;
            

            // Process each "mailingChunks" chunk as a separate Task.
            Task sendMailingChunks = Task.Run(() => SendEmails(mailingList, emailPerson));

            // Create a Task that will complete when emails were sent to all the "mailingList".
            Task sendBuilkEmails = Task.WhenAll(sendMailingChunks);

            // Displaying the progress of bulk email sending.
            while (!sendBuilkEmails.IsCompleted)
            {
                Console.WriteLine($"{SentEmailCounter,5} emails have been sent!");
                Task.Delay(1000).Wait();
            }
        }

        static void SendEmails(string recipients, EmailPerson emailPerson)
        {
            using (var smtp = new SmtpClient(Host))
            {
                smtp.Connect();
                smtp.Authenticate(Username, Password);

                    MailMessage message = new MailMessage(Sender, recipients)
                    {
                        Subject = emailPerson.Title,
                        BodyText = "Witaj " + emailPerson.FirstName + " " + emailPerson.LastName + "!"
                        + "\n"+ emailPerson.Message
                    };

                    smtp.SendMessage(message);
                    Interlocked.Increment(ref SentEmailCounter);
               
            }
        }

        static List<List<string>> SplitMany(List<string> source, int size)
        {
            var sourceChunks = new List<List<string>>();

            for (int i = 0; i < source.Count; i += size)
                sourceChunks.Add(source.GetRange(i, Math.Min(size, source.Count - i)));

            return sourceChunks;
        }
    }
}

