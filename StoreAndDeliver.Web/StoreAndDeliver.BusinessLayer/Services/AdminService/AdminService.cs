using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Repositories.BackupRepository;
using StoreAndDeliver.DataLayer.Repositories.LogsRepository;
using System;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IBackupRepository _backupRepository;
        private readonly ILogsRepository _logsRepository;

        public AdminService(IBackupRepository backupRepository, ILogsRepository logsRepository)
        {
            _backupRepository = backupRepository;
            _logsRepository = logsRepository;
        }

        public async Task BackupDatabase(string connectionString)
        {
            await _backupRepository.BackupDatabase(connectionString);
        }

        public async Task<LogsDto> GetLogs(DateTime logDate)
        {
            string content = await _logsRepository.GetLogs(logDate);
            return new LogsDto
            {
                Content = content,
                Date = logDate
            };
        }
    }
}
