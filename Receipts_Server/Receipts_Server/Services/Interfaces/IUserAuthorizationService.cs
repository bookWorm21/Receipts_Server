using Models.Entities;
using Models.Request;
using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.Services.Interfaces
{
    public interface IUserAuthorizationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        RegistrationResponse Register(OwnerRegisterData owner);

        Owner GetById(int id);
    }
}
