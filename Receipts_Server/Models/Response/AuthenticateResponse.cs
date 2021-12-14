using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Response
{
    public class AuthenticateResponse
    {
        public int OwnerId { get; set; }

        public string Error { get; set; }

        public bool IsSuccessfull { get; set; }

        public string Token { get; set; }

        public AuthenticateResponse()
        {
            Token = "";
            Error = "";
            IsSuccessfull = false;
        }

        public AuthenticateResponse(string error, bool isSuccessfull, string token)
        {
            Error = error;
            IsSuccessfull = isSuccessfull;
            Token = token;
        }

        public AuthenticateResponse(bool isSuccessfull, string error)
        {
            Error = error;
            IsSuccessfull = isSuccessfull;
        }

        public AuthenticateResponse(string token, int ownerId)
        {
            OwnerId = ownerId;
            Token = token;
            IsSuccessfull = true;
        }
    }
}
