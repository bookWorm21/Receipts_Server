using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities
{
    public class TariffPlan
    {
        [Key]
        public int TariffPlanId { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public int TariffId { get; set; }

        public Tariff Tariff { get; set; }
    }
}