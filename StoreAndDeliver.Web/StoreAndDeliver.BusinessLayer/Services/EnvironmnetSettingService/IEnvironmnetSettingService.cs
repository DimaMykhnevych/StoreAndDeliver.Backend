using StoreAndDeliver.BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.EnvironmnetSettingService
{
    public interface IEnvironmnetSettingService
    {
        public Task<IEnumerable<EnvironmentSettingDto>> GetEnvironmentSettings();
    }
}
