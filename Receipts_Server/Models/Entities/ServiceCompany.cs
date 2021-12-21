using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class ServiceCompany
    {
        [Key]
        public int ServiceCompanyId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string Mail { get; set; }
        
        public ICollection<Service> Services { get; set; }
    }
}