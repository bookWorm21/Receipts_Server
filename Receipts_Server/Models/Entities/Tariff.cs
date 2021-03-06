using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Tariff
    {
        [Key]
        public int TariffId { get; set; }

        [Required]
        public DateTime BeginData { get; set; }

        [Required]
        public DateTime EndData { get; set; }

        [Required]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal Volume{ get; set; }

        public ICollection<TariffPlan> TariffPlans { get; set; }
    }
}
