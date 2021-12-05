using System;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace StoreAndDeliver.DataLayer.Repositories.BackupRepository
{
    public class BackupRepository : IBackupRepository
    {
        private readonly string _backupFolderPath = @"StoreAndDeliver\Backup";
        private readonly string _appDataFolder = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}";
        private readonly ILogger _logger;

        public BackupRepository(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger("BackupRepository");
        }

        public async Task<Stream> BackupDatabase(string connectionString)
        {
            string fullPathToBackupFolder = Path.Combine(_appDataFolder, _backupFolderPath);
            _logger.LogInformation($"fullPathToBackupFolder: {fullPathToBackupFolder}");
            if (!Directory.Exists(fullPathToBackupFolder))
            {
                Directory.CreateDirectory(fullPathToBackupFolder);
            }
            MySqlConnector.MySqlConnectionStringBuilder sqlConnectionStringBuilder = new MySqlConnector.MySqlConnectionStringBuilder(connectionString);
            string backupFileName = $"{sqlConnectionStringBuilder.Database}-{DateTime.Now:yyyy-MM-dd}.sql";
            string pathToBackupFile = Path.Combine(fullPathToBackupFolder, backupFileName);
            if (File.Exists(pathToBackupFile))
            {
                _logger.LogInformation($"Deleting backup file as it already exists: {pathToBackupFile}");
                File.Delete(pathToBackupFile);
            }
            _logger.LogInformation($"pathToBackupFile: {pathToBackupFile}");
            if (sqlConnectionStringBuilder.Server.Contains("localhost"))
            {
                using MySqlConnector.MySqlConnection connection = new MySqlConnector.MySqlConnection(sqlConnectionStringBuilder.ConnectionString);
                string query = $"mysqldump -u {sqlConnectionStringBuilder.UserID} {sqlConnectionStringBuilder.Database} > {pathToBackupFile}";
                using Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = $@"{await GetMySqlWorkingDirectory(connectionString)}/bin";
                process.Start();
                var streamWriter = process.StandardInput;
                streamWriter.WriteLine(query);
                streamWriter.Close();
                await process.WaitForExitAsync();
                process.Close();
            }
            else
            {
                using MySqlConnection conn = new MySqlConnection(connectionString);
                using MySqlCommand cmd = new MySqlCommand();
                using MySqlBackup mb = new MySqlBackup(cmd);
                cmd.Connection = conn;
                _logger.LogInformation($"Start backup creation");
                conn.Open();
                mb.ExportToFile(pathToBackupFile);
                conn.Close();
                _logger.LogInformation($"Backup was created successfully");
            }
            Stream fs = File.OpenRead(pathToBackupFile);
            return fs;
        }

        private static async Task<string> GetMySqlWorkingDirectory(string connectionString)
        {
            using var con = new MySqlConnector.MySqlConnection(connectionString);
            con.Open();
            using MySqlConnector.MySqlCommand command = new("select @@basedir as dir", con);
            using DbDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            return reader.GetString(0);
        }
    }
}
