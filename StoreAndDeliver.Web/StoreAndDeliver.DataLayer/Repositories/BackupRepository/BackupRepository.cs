using MySqlConnector;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.BackupRepository
{
    public class BackupRepository : IBackupRepository
    {
        private readonly string _backupFolderPath = @"StoreAndDeliver\Backup";
        private readonly string _appDataFolder = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}";
        public async Task BackupDatabase(string connectionString)
        {
            string fullPathToBackupFolder = Path.Combine(_appDataFolder, _backupFolderPath);
            if (!Directory.Exists(fullPathToBackupFolder))
            {
                Directory.CreateDirectory(fullPathToBackupFolder);
            }
            MySqlConnectionStringBuilder sqlConnectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
            string backupFileName = $"{sqlConnectionStringBuilder.Database}-{DateTime.Now:yyyy-MM-dd}.sql";
            string pathToBackupFile = Path.Combine(fullPathToBackupFolder, backupFileName);
            if (File.Exists(pathToBackupFile))
            {
                File.Delete(pathToBackupFile);
            }
            using MySqlConnection connection = new MySqlConnection(sqlConnectionStringBuilder.ConnectionString);
            string query = $"mysqldump -u {sqlConnectionStringBuilder.UserID} {sqlConnectionStringBuilder.Database} > {pathToBackupFile}";
            using Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WorkingDirectory = $@"{await GetMySqlWorkingDirectory(connectionString)}\bin";
            process.Start();
            var streamWriter = process.StandardInput;
            streamWriter.WriteLine(query);
            streamWriter.Close();
            await process.WaitForExitAsync();
            process.Close();
        }

        private async Task<string> GetMySqlWorkingDirectory(string connectionString)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();
            using MySqlCommand command = new("select @@basedir as dir", con);
            using DbDataReader reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();
            return reader.GetString(0);
        }
    }
}
