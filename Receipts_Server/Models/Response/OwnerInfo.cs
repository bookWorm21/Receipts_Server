using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Response
{
    public class OwnerInfo
    {
        public string Login { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }
    }
}