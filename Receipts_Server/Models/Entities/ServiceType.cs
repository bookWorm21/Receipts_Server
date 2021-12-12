using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Entities
{
    public class ServiceType
    {
        [Key]
        public int ServiceTypeId { get; set; }

        [Required]
        [MaxLength(30)]
        public string ServiceTypeName { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
