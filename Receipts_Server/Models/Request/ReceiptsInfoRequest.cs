using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Request
{
    public class ReceiptsInfoRequest
    {
        public ReceiptStatusFilter Status { get; set; }
    }

    public enum ReceiptStatusFilter : byte
    {
        Any,
        NotPaid,
        Paid
    }
}
