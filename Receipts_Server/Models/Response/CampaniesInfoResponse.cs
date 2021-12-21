using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Response
{
    public class CampaniesInfoResponse
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Mail { get; set; }

        public List<string> ServiceTypes { get; set; }
    }
}
