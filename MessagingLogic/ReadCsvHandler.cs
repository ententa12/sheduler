using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSVEmailModel;
using MediatR;
using SchedulerLogic;

namespace CSVReaderLogic
{
    public class ReadCsvHandler : IRequestHandler<ReadCsvRequest, List<EmailPerson>>
    {
        public Task<List<EmailPerson>> Handle(ReadCsvRequest request, CancellationToken cancellationToken)
        {
            var res = new CsvEmailReader<EmailPerson>().ReadFile(request.Path, request.Count, request.ToSkip);
            return Task.FromResult(res);
        }
    }
}