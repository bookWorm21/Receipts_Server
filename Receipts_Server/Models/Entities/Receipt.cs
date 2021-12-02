using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Receipt
    {
        [Key]
        public int ReceiptId { get; set; }

        [Required]
        public DateTime ChargeDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Volume { get; set; }

        public int ServiceId { get; set; }

        public Service Service {get; set;}

        public int PropertyId { get; set; }

        public Property Property { get; set; }
    }
}