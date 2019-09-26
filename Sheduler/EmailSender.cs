using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GemBox.Email;
using GemBox.Email.Smtp;
using NLog;

namespace Sheduler
{
    public static class EmailSender
    {

        const string Host = "smtp.gmail.com";
        const string Username = "scheduler.ztp";
        const string Password = "ZAQ!2wsx";
        const string Sender = "scheduler.ztp@gmail.com";
        static int SentEmailCounter = 0;

        public async static Task SendEmail()
        {
            Logger logger = LogManager.GetLogger("fileLogger");
            // If using Professional version, put your serial key below.
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var mailingList = new List<string>() {
                "ententa12@gmail.com"
            };

            int chunkSize = 80;

            // Split "mailingList" to multiple lists of "chunkSize" size.
            var mailingChunks = SplitMany(mailingList, chunkSize);

            // Process each "mailingChunks" chunk as a separate Task.
            IEnumerable<Task> sendMailingChunks = mailingChunks.Select(
                mailingChunk => Task.Run(() => SendEmails(mailingChunk)));

            // Create a Task that will complete when emails were sent to all the "mailingList".
            Task sendBuilkEmails = Task.WhenAll(sendMailingChunks);
            logger.Info("Email was sended to: " + mailingList[0]);
            await sendBuilkEmails;
            // Displaying the progress of bulk email sending.
        }

        static void SendEmails(IEnumerable<string> recipients)
        {
            using (var smtp = new SmtpClient(Host))
            {
                smtp.Connect();
                smtp.Authenticate(Username, Password);

                foreach (var recipient in recipients)
                {
                    MailMessage message = new MailMessage(Sender, recipient)
                    {
                        Subject = "New NEW",
                        BodyText = "Dear reader,\n" +
                            "We have released a new blog post.\n" +
                            "You can find it on: https://www.gemboxsoftware.com/company/blog"
                    };

                    smtp.SendMessage(message);
                    Interlocked.Increment(ref SentEmailCounter);
                }
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

