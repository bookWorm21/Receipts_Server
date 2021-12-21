using Models.Entities;
using Models.Request;
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
                        .Join(_dbContext.TariffPlans, rec => rec.rec.ServiceId, plan => plan.ServiceId, (rec, plan) => new { rec, plan })
                        .Join(_dbContext.Tariffs, rec => rec.plan.TariffId, tar => tar.TariffId, (rec, tar) => new { rec, tar })
                        .Where(p => p.rec.rec.rec.PropertyId == property.pr.pr.PropertyId)
                        .Where(p => p.rec.rec.rec.Status == false)
                        .Where(p => p.rec.rec.rec.ChargeDate >= p.tar.BeginData && p.rec.rec.rec.ChargeDate <= p.tar.EndData)
                        .Sum(s => s.tar.Volume * s.rec.rec.rec.Volume);

                    results.Add(propertyInfo);
                }
            }
            return results.ToArray();
        }

        public ServiceTypeInfo[] GetServiceTypes()
        {
            return _dbContext.ServiceTypes
                .Select(p => new ServiceTypeInfo() { Id = p.ServiceTypeId, Name = p.ServiceTypeName }).ToArray();
        }

        public CampaniesInfoResponse[] GetCompanies(CampaniesInfoRequest request, int ownerId)
        {
            if (request == null)
                return null;

            List<CampaniesInfoResponse> companies = new List<CampaniesInfoResponse>();

            if (request.AllCampanies)
            {
                foreach (var company in _dbContext
                    .ServiceCompanies
                    .Join(_dbContext.Services, com => com.ServiceCompanyId, ser => ser.ServiceCompanyId, (com, ser) => new { com, ser })
                    .Join(_dbContext.ServiceTypes, com => com.ser.ServiceTypeId, typ => typ.ServiceTypeId, (com, typ) => new { com, typ })
                    .Where(p => request.AllServiceTypes || p.typ.ServiceTypeId == request.ServiceTypeId)
                    .Where(p => p.com.com.Name.ToLower().Contains(request.Substring.ToLower()))
                    .AsEnumerable()
                    .GroupBy(p => new { p.com.com.ServiceCompanyId, p.com.com.Name, p.com.com.Mail, p.com.com.PhoneNumber }))
                {
                    var addingCompany = new CampaniesInfoResponse();
                    addingCompany.Number = company.Key.ServiceCompanyId.ToString();
                    addingCompany.Name = company.Key.Name;
                    addingCompany.PhoneNumber = company.Key.PhoneNumber;
                    addingCompany.Mail = company.Key.Mail;
                    addingCompany.ServiceTypes = company.Select(p => p.typ.ServiceTypeName).ToList();
                    companies.Add(addingCompany);
                }
            }
            else
            {
                foreach (var company in _dbContext
                    .Properties
                    .Join(_dbContext.Receipts, prop => prop.PropertyId, rec => rec.PropertyId, (prop, rec) => new { prop, rec })
                    .Join(_dbContext.Services, rec => rec.rec.ServiceId, ser => ser.ServiceId, (rec, ser) => new { rec, ser })
                    .Join(_dbContext.ServiceCompanies, rec => rec.ser.ServiceCompanyId, com => com.ServiceCompanyId, (rec, com) => new { rec, com })
                    .Join(_dbContext.ServiceTypes, rec => rec.rec.ser.ServiceTypeId, typ => typ.ServiceTypeId, (rec, typ) => new { rec, typ })
                    .Where(p => p.rec.rec.rec.prop.OwnerId == ownerId)
                    .Where(p=>p.rec.com.Name.ToLower().Contains(request.Substring.ToLower()))
                    .Where(p => request.AllServiceTypes || p.typ.ServiceTypeId == request.ServiceTypeId)
                    .AsEnumerable()
                    .GroupBy(p => new { p.rec.com.ServiceCompanyId, p.rec.com.Name, p.rec.com.Mail, p.rec.com.PhoneNumber }))
                {
                    var addingCompany = new CampaniesInfoResponse();
                    addingCompany.Number = company.Key.ServiceCompanyId.ToString();
                    addingCompany.Name = company.Key.Name;
                    addingCompany.PhoneNumber = company.Key.PhoneNumber;
                    addingCompany.Mail = company.Key.Mail;
                    addingCompany.ServiceTypes = company.Select(p => p.typ.ServiceTypeName).ToList();
                    companies.Add(addingCompany);
                }
            }

            return companies.ToArray();
        }
    }
}
