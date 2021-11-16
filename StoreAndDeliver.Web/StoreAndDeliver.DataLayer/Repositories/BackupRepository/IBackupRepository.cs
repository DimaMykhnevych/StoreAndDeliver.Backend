using System.Threading.Tasks;

namespace StoreAndDeliver.DataLayer.Repositories.BackupRepository
{
    public interface IBackupRepository
    {
        Task BackupDatabase(string connectionString);
    }
}
