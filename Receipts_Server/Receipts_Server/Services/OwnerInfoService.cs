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
    }
}
