using WindowsService;

namespace Sheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            new Configure().ConfigureService();
        }
    }
}