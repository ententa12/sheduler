using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using GemBox.Email;
using GemBox.Email.Smtp;
using NLog;
using Logger = NLogger.Logger;

namespace EmailSenderLogic
{
    public class EmailSender
    {
        ILogger _logger;

        public EmailSender()
        {
            _logger = new Logger().GetLogger();
            ConfigurationManager.OpenExeConfiguration("C://Users//KiszczakPatryk//Documents//Visual Studio 2017//Projects//Sheduler//EmailSender//App.config");
            ConfigurationManager.AppSettings.Get("Host");
        }

        string Host = ConfigurationManager.AppSettings["Key1"];
        string Username = ConfigurationManager.AppSettings["username"];
        string Password = ConfigurationManager.AppSettings["password"];
        string Sender = ConfigurationManager.AppSettings["sender"];

        static int SentEmailCounter = 0;

        public async Task SendEmail(EmailPerson emailPerson)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            var mail = emailPerson.Email;
            Task sendMailingChunks = Task.Run(() => SendEmails(mail, emailPerson));
            Task sendBuilkEmails = Task.WhenAll(sendMailingChunks);
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