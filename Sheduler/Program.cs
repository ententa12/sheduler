using WindowsService;

namespace StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Configure().ConfigureService();
        }
    }
}