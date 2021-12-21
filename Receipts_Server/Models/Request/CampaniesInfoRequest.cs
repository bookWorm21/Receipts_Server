using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Request
{
    public class CampaniesInfoRequest
    {
        public string Substring { get; set; }

        public bool AllCampanies { get; set; }

        public bool AllServiceTypes { get; set; }

        public int ServiceTypeId { get; set; }
    }
}
