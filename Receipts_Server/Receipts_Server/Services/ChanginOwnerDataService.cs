using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Models.Entities;
using Models.Request;
using Models.Response;
using Receipts_Server.DataBaseContext;
using Receipts_Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Receipts_Server.Services
{
    public class ChanginOwnerDataService : IChangingOwnerDataService
    {
        private AppDbContext _dbContext;

        public ChanginOwnerDataService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RegistrationResponse ChangeData(ChangingOwnerData ownerData, int ownerId)
        {
            RegistrationResponse response = new RegistrationResponse();
            response.IsSuccessfull = false;

            if (ownerData.Login.Length == 0 || ownerData.FirstName.Length == 0 || ownerData.LastName.Length == 0
                || ownerData.Password.Length == 0)
            {
                response.Error = "Поля не должны быть пустыми";
            }
            else if(ownerData.Login.Contains(' ') || ownerData.FirstName.Contains(' ') || ownerData.LastName.Contains(' ')
                || ownerData.Password.Contains(' ') || ownerData.Patronymic.Contains(' '))
            {
                response.Error = "Поля не должны содержать пробелов";
            }
            else
            {
                Owner owner = _dbContext.Owners.FirstOrDefault(p => p.OwnerId == ownerId);
                if(owner != null)
                {
                    if (_dbContext.Owners.FirstOrDefault(p => p.Login == ownerData.Login 
                    && p.OwnerId != owner.OwnerId) != null)
                    {
                        response.Error = "Этот логин уже используется";
                    }
                    else
                    {
                        var passwordHash = Convert.ToBase64String(Pbkdf2(ownerData.Password, Convert.FromBase64String(owner.Salt)));
                        if (owner.PasswordHash == passwordHash)
                        {
                            owner.Login = ownerData.Login;
                            owner.FirstName = ownerData.FirstName;
                            owner.LastName = ownerData.LastName;
                            owner.Patronymic = ownerData.Patronymic;

                            _dbContext.SaveChanges();

                            response.IsSuccessfull = true;
                        }
                        else
                        {
                            response.Error = "Не верный пароль";
                        }
                    }
                }
                else
                {
                    response.Error = "Не удалось изменить данные";
                }
            }

            return response;
        }

        public RegistrationResponse ChangePassword(ChangingOwnerPassword ownerPasswords, int ownerId)
        {
            RegistrationResponse response = new RegistrationResponse();
            response.IsSuccessfull = false;

            if (ownerPasswords.OldPassword.Length == 0 || ownerPasswords.NewPassword.Length == 0)
            {
                response.Error = "Поля не должны быть пустыми";
            }
            else if (ownerPasswords.OldPassword.Contains(' ') || ownerPasswords.NewPassword.Contains(' '))
            {
                response.Error = "Поля не должны содержать пароля";
            }
            else
            {
                Owner owner = _dbContext.Owners.FirstOrDefault(p => p.OwnerId == ownerId);
                if (owner != null)
                {
                    var passwordHash = Convert.ToBase64String(Pbkdf2(ownerPasswords.OldPassword, Convert.FromBase64String(owner.Salt)));
                    if (owner.PasswordHash == passwordHash)
                    {
                        var salt = GetRandomBytes();
                        owner.PasswordHash = Convert.ToBase64String(Pbkdf2(ownerPasswords.NewPassword, salt));
                        owner.Salt = Convert.ToBase64String(salt);

                        _dbContext.SaveChanges();

                        response.IsSuccessfull = true;
                    }
                    else
                    {
                        response.Error = "Не верный пароль";
                    }
                }
                else
                {
                    response.Error = "Не удалось изменить данные";
                }
            }

            return response;
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
