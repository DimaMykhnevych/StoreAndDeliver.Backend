using StoreAndDeliver.BusinessLayer.DTOs;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AdminService
{
    public interface IAdminService
    {
        Task BackupDatabase(string connectionString);
        Task<LogsDto> GetLogs(DateTime logDate);
    }
}
