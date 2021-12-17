using Models.Entities;
using Models.Response;
using Receipts_Server.DataBaseContext;
using Receipts_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.Services
{
    public class OwnerInfoService : IOwnerInfoService
    {
        private AppDbContext _dbContext;

        public OwnerInfoService(AppDbContext context)
        {
            _dbContext = context;
        }

        public OwnerInfo GetOwnerInfo(int ownerId)
        {
            OwnerInfo info = null;
            Owner owner = _dbContext.Owners.FirstOrDefault(p => p.OwnerId == ownerId);
            if(owner != null)
            {
                info = new OwnerInfo();
                info.FirstName = owner.FirstName;
                info.LastName = owner.LastName;
                info.Login = owner.Login;
                info.Patronymic = owner.Patronymic;
            }

            return info;
        }

        public PropertiesInfo[] GetOwnerProperties(int ownerId)
        {
            List<PropertiesInfo> results = new List<PropertiesInfo>();
            Owner owner = _dbContext.Owners.FirstOrDefault(p => p.OwnerId == ownerId);
            if(owner!= null)
            {
                var propetries = _dbContext
                    .Properties
                    .Join(_dbContext.Owners, pr => pr.OwnerId, own => own.OwnerId, (pr, own) => new { pr, own })
                    .Join(_dbContext.Cities, pr => pr.pr.CityId, cit => cit.CityId, (pr, cit) => new { pr, cit })
                    .Where(p => p.pr.own.OwnerId == owner.OwnerId).ToList();

                foreach (var property in propetries)
                {
                    var focusingProp = property.pr.pr;
                    PropertiesInfo propertyInfo = new PropertiesInfo();
                    propertyInfo.Number = focusingProp.PropertyId;
                    propertyInfo.Square = focusingProp.Square;
                    propertyInfo.Address = string.Concat
                        (property.cit.Name, ", ", focusingProp.Street, " ",
                        focusingProp.HouseNumber, ", ", focusingProp.ApartmentNumber);

                    propertyInfo.Debt = _dbContext
                        .Receipts
                        .Join(_dbContext.Services, rec => rec.ServiceId, ser => ser.ServiceId, (rec, ser) => new { rec, ser })
                        .Join(_dbContext.Tariffs, rec => rec.ser.ServiceId, tar => tar.ServiceId, (rec, tar) => new { rec, tar })
                        .Where(p => p.rec.rec.PropertyId == property.pr.pr.PropertyId)
                        .Where(p => p.rec.rec.Status == false)
                        .Where(p => p.rec.rec.ChargeDate >= p.tar.BeginData && p.rec.rec.ChargeDate <= p.tar.EndData)
                        .Sum(s => s.tar.Volume * s.rec.rec.Volume);

                    results.Add(propertyInfo);
                }
            }
            return results.ToArray();
        }
    }
}
