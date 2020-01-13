using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CSVEmailModel;
using EmailSenderInterface;
using FluentEmail.Core;
using FluentEmail.Smtp;
using NLog;

namespace FluentEmailSender
{
    public class FluentSender : IEmailSender<EmailPerson>
    {
        private readonly ILogger _logger;

        public FluentSender(ILogger logger)
        {
            _logger = logger;
        }

        public async Task SendEmail(EmailPerson emailPerson)
        {
            _logger.Info("Email sender const");
            Email.DefaultSender = new SmtpSender(SetSmtpClient());
            var email = Email
                .From(ConfigurationManager.AppSettings["sender"])
                .To(emailPerson.Email)
                .Subject(emailPerson.Title)
                .Body($"Witaj {emailPerson.FirstName} {emailPerson.LastName}!\n{emailPerson.Message}");
            var res = await email.SendAsync();
            if (!res.Successful)
            {
                _logger.Error(res.ErrorMessages);
            }
            else
            {
                _logger.Info("Mail sent to {0} id: {1}", emailPerson.Email, emailPerson.Id);
            }
        }

        private SmtpClient SetSmtpClient()
        {
            try
            {
                return new SmtpClient(ConfigurationManager.AppSettings["host"],
                    int.Parse(ConfigurationManager.AppSettings["smtpPort"]))
                {
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"],
                        ConfigurationManager.AppSettings["password"]),
                    EnableSsl = true,
                };
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
        }
    }
}