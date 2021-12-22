using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Request
{
    public class ReceiptsInfoRequest
    {
        public ReceiptStatusFilter Status { get; set; }

        public bool AllServices { get; set; }

        public int ServiceId { get; set; }

        public bool AllProperties { get; set; }

        public int PropertyId { get; set; }
    }

    public enum ReceiptStatusFilter : byte
    {
        Any,
        NotPaid,
        Paid
    }
}
