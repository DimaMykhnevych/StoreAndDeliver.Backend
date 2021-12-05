using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AdminService
{
    public interface IAdminService
    {
        Task<Stream> BackupDatabase(string connectionString);
        Task<LogsDto> GetLogs(DateTime logDate);
    }
}
