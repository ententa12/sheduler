using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using EmailSenderInterface;
using GemBox.Email;
using GemBox.Email.Smtp;
using NLog;

namespace EmailSenderLogic
{
    public class EmailSender : IEmailSender<EmailPerson>
    {
        ILogger _logger;

        public EmailSender()
        {
            var c = ConfigurationManager.AppSettings;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            _logger = LogManager.GetLogger("fileLogger");
        }

        readonly string Host = ConfigurationManager.AppSettings["host"];
        readonly string Username = ConfigurationManager.AppSettings["username"];
        readonly string Password = ConfigurationManager.AppSettings["password"];
        readonly string Sender = ConfigurationManager.AppSettings["sender"];

        static int SentEmailCounter = 0;

        public async Task SendEmail(EmailPerson emailPerson)
        {
            var mail = emailPerson.Email;
            var sendMailingChunks = Task.Run(() => SendEmails(mail, emailPerson));
            var sendBuilkEmails = Task.WhenAll(sendMailingChunks);
            _logger.Info("Mail sended to {0}", emailPerson.Email);
            await sendBuilkEmails;
        }

        void SendEmails(string recipients, EmailPerson emailPerson)
        {
            try
            {
                using (var smtp = new SmtpClient(Host))
                {
                    smtp.ConnectTimeout = TimeSpan.FromSeconds(20);
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
                _logger.Error(ex);
            }
        }
    }
}