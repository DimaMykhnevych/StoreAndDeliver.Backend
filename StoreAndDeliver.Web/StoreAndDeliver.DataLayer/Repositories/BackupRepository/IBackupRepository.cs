using System.IO;
using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.BackupRepository
{
    public interface IBackupRepository
    {
        Task<Stream> BackupDatabase(string connectionString);
    }
}
