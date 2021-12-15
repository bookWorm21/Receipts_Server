using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Request
{
    public class ChangingOwnerData
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
