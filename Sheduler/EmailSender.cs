using System;
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

        public static async Task SendEmail(EmailPerson emailPerson)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var mail = emailPerson.Email;
            
            Task sendMailingChunks = Task.Run(() => SendEmails(mail, emailPerson));
            
            Task sendBuilkEmails = Task.WhenAll(sendMailingChunks);
            await sendBuilkEmails;
        }

        static void SendEmails(string recipients, EmailPerson emailPerson)
        {
            try
            {
                using (var smtp = new SmtpClient(Host))
                {
                    smtp.ConnectTimeout = TimeSpan.FromSeconds(10);
                    smtp.Connect();
                    smtp.Authenticate(Username, Password);

                    MailMessage message = new MailMessage(Sender, recipients)
                    {
                        Subject = emailPerson.Title,
                        BodyText = "Witaj " + emailPerson.FirstName + " " + emailPerson.LastName + "!"
                        + "\n" + emailPerson.Message
                    };

                    smtp.SendMessage(message);
                    Interlocked.Increment(ref SentEmailCounter);

                }
            } catch(Exception ex)
            {
                LoggerUtils.logger.Error(ex);
            }
        }
    }
}

