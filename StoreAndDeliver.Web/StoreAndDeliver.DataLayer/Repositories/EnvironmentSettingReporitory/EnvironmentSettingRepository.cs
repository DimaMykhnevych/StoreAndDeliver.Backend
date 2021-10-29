using StoreAndDeliver.DataLayer.DbContext;
using StoreAndDeliver.DataLayer.Models;

namespace StoreAndDeliver.DataLayer.Repositories.EnvironmentSettingReporitory
{
    public class EnvironmentSettingRepository : Repository<EnvironmentSetting>, IEnvironmentSettingRepository
    {
        public EnvironmentSettingRepository(StoreAndDeliverDbContext context) : base(context)
        {
        }
    }
}
