using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        [Required]
        [MaxLength(30)]
        public string Street { get; set; }

        [Required]
        [MaxLength(30)]
        public int HouseNumber { get; set; }

        [Required]
        public int Square { get; set; }

        public int OwnerId { get; set; }

        public Owner Owner { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}
