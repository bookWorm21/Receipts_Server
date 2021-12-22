using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Response
{
    public class ReceiptInfoResponse
    {
        public int Number { get; set; }

        public string CompanyName { get; set; }

        public string ServiceTypeName { get; set; }

        public decimal TariffVolume { get; set; }

        public decimal ServiceVolume { get; set; }

        public decimal Sum { get; set; }

        public bool Status { get; set; }

        public int PropertyId { get; set; }

        public string PropertyAddress { get; set; }

        public DateTime ReceiptDate { get; set; }
    }
}
