using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Request
{
    public class AuthenticateRequest
    {
        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
