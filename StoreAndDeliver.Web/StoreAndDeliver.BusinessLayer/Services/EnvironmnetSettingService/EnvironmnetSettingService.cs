using AutoMapper;
using StoreAndDeliver.BusinessLayer.DTOs;
using StoreAndDeliver.DataLayer.Models;
using StoreAndDeliver.DataLayer.Repositories.EnvironmentSettingReporitory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAndDeliver.BusinessLayer.Services.EnvironmnetSettingService
{
    public class EnvironmnetSettingService : IEnvironmnetSettingService
    {
        private readonly IEnvironmentSettingRepository _environmentSettingRepository;
        private readonly IMapper _mapper;

        public EnvironmnetSettingService(
            IEnvironmentSettingRepository environmentSettingRepository,
            IMapper mapper)
        {
            _environmentSettingRepository = environmentSettingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EnvironmentSettingDto>> GetEnvironmentSettings()
        {
            IEnumerable<EnvironmentSetting> settings = await _environmentSettingRepository.GetAll();
            return _mapper.Map<IEnumerable<EnvironmentSettingDto>>(settings);
        }
    }
}
