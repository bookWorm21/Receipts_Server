using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Response
{
    public class AuthenticateResponse
    {   
        public List<string> Errors = new List<string>();

        public bool IsSuccessfull => Errors.Count == 0;

        public string Token { get; set; }

        public AuthenticateResponse()
        {

        }

        public AuthenticateResponse(string token)
        {
            Token = token;
        }
    }
}
