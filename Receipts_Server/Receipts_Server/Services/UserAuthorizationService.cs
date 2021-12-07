using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Models.Request;
using Models.Response;
using Receipts_Server.DataBaseContext;
using Receipts_Server.Helpers.Extension;
using Receipts_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Receipts_Server.Services
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserAuthorizationService(AppDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            if(request == null)
            {
                return null;
            }

            AuthenticateResponse response;

            var owner = _dbContext
                .Owners
                .FirstOrDefault(p => p.Login == request.Login);

            if (owner != null)
            {
                var passwordHash = Convert.ToBase64String(Pbkdf2(request.Password, Convert.FromBase64String(owner.Salt)));
                if (passwordHash != owner.PasswordHash)
                {
                    response = new AuthenticateResponse(false, "Uncorrect login or password");
                    return response;
                }
            }
            else
            {
                response = new AuthenticateResponse(false, "Uncorrect login or password");
                return response;
            }

            var token = _configuration.GenerateJwtToken(owner);
            return new AuthenticateResponse(token);
        }


        public Owner GetById(int id)
        {
            return _dbContext.Owners.FirstOrDefault(p => p.OwnerId == id);
        }

        public Task<AuthenticateResponse> Register(OwnerRegisterData owner)
        {
            throw new NotImplementedException();
        }

        private byte[] GetRandomBytes(int size = 32)
        {
            var salt = new byte[size];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }

        private byte[] Pbkdf2(string password, byte[] salt, int numBytes = 32)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: numBytes
                );
        }
    }
}
