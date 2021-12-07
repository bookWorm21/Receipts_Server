using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public ICollection<Tariff> Tariffs { get; set; }

        public int ServiceCompanyId { get; set; }

        public ServiceCompany ServiceCompany { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}
