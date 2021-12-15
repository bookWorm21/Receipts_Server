using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.Services.Interfaces
{
    public interface IChangingOwnerDataService
    {
        RegistrationResponse ChangeData(ChangingOwnerData ownerData, int ownerId);

        RegistrationResponse ChangePassword(ChangingOwnerPassword ownerPasswords, int ownerId);
    }
}