using ITravel.Data.IRepositories;
using ITravel.Domain.Entities.SoftSettings;
using ITravel.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _repository;

        public SettingService(IUnitOfWork unitOfWork)
        {
            this._repository = unitOfWork;
        }

        public async Task<SoftSettings> GetAsync()
        {
            return await _repository.SoftSetting.FirstOrDefaultAsync(that => that.Id > 0);

        }
    }
}
