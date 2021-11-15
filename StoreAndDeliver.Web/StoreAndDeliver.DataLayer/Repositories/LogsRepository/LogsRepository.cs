using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.LogsRepository
{
    public class LogsRepository : ILogsRepository
    {
        private readonly string _logsFolderPath = @"StoreAndDeliver\logs";
        private readonly string _appDataFolder = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}";

        public async Task<string> GetLogs(DateTime date)
        {
            string fullPathToLogsFolder = Path.Combine(_appDataFolder, _logsFolderPath);
            if (!Directory.Exists(fullPathToLogsFolder))
            {
                Directory.CreateDirectory(fullPathToLogsFolder);
            }
            string[] fileEntries = Directory.GetFiles(fullPathToLogsFolder);
            string pattern = $"{date:yyyyMMdd}";
            string neededDateLogsFileName = fileEntries.FirstOrDefault(f => f.Contains(pattern));
            if (string.IsNullOrEmpty(neededDateLogsFileName))
            {
                return "";
            }
            string fullPathToLogFolder = Path.Combine(fullPathToLogsFolder, neededDateLogsFileName);
            //string content = await File.ReadAllTextAsync(fullPathToLogFolder);
            string content = string.Empty;
            var fs = new FileStream(fullPathToLogFolder, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                content += await sr.ReadToEndAsync();
            }
            return content;
        }
    }
}
