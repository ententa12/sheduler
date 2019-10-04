using System.Threading.Tasks;

namespace EmailSenderInterface
{
    public interface IEmailSender<T>
    {
        Task SendEmail(T emailPerson);
    }
}