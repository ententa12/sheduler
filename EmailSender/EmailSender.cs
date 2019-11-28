using System;
using System.Configuration;
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
        private readonly ILogger _logger;

        public EmailSender()
        {
            var c = ConfigurationManager.AppSettings;
            ComponentInfo.SetLicense(ConfigurationManager.AppSettings["license"]);
            _logger = LogManager.GetLogger("fileLogger");
        }

        private readonly string _host = ConfigurationManager.AppSettings["host"];
        private readonly string _username = ConfigurationManager.AppSettings["username"];
        private readonly string _password = ConfigurationManager.AppSettings["password"];
        private readonly string _sender = ConfigurationManager.AppSettings["sender"];

        public async Task SendEmail(EmailPerson emailPerson)
        {
            var mail = emailPerson.Email;
            await new Task(() => SendEmails(mail, emailPerson));
            _logger.Info("Mail sent to {0}", emailPerson.Email);
        }

        void SendEmails(string recipients, EmailPerson emailPerson)
        {
            try
            {
                using (var smtp = new SmtpClient(_host))
                {
                    smtp.ConnectTimeout = TimeSpan.FromSeconds(20);
                    smtp.Connect();
                    smtp.Authenticate(_username, _password);
                    
                    var message = new MailMessage(_sender, recipients)
                    {
                        Subject = emailPerson.Title,
                        BodyText = $"Witaj {emailPerson.FirstName} {emailPerson.LastName}!\n{emailPerson.Message}"
                    };

                    smtp.SendMessage(message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}