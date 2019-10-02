using System;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace EmailSenderLogic
{
    public class EmailSender
    {
        public EmailSender()
        {
        }

         string Host = ConfigurationSettings.AppSettings["host"];
         string Username = ConfigurationSettings.AppSettings["username"];
         string Password = ConfigurationSettings.AppSettings["password"];
         string Sender = ConfigurationSettings.AppSettings["sender"];

        static int SentEmailCounter = 0;

        public async Task SendEmail(EmailPerson emailPerson)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var mail = emailPerson.Email;

            Task sendMailingChunks = Task.Run(() => SendEmails(mail, emailPerson));

            Task sendBuilkEmails = Task.WhenAll(sendMailingChunks);
            await sendBuilkEmails;
        }

        void SendEmails(string recipients, EmailPerson emailPerson)
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
            }
            catch (Exception ex)
            {
                //LoggerUtils.logger.Error(ex);
            }
        }
    }
}