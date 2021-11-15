using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.LogsRepository
{
    public interface ILogsRepository
    {
        Task<string> GetLogs(DateTime date);
    }
}
