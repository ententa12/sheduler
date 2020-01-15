namespace SchedulerLogic
{
    public class ReadCsvRequest
    {
        public int ToSkip { get; }
        public int Count { get; }
        public string Path { get; }

        public ReadCsvRequest(string path, int skip, int count)
        {
            ToSkip = skip;
            Count = count;
            Path = path;
        }
    }
}