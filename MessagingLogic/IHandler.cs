using System.Threading;
using System.Threading.Tasks;

namespace MessagingLogic
{
    public interface IHandler<in T> where T : IMessage
    {
        Task HandleAsync(T message, CancellationToken token);
    }
}