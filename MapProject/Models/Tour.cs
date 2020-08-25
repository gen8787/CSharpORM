using System.ComponentModel.DataAnnotations;

namespace MapProject.Models
{
    public class Tour
        {
// Primary Key
        [Key]
        public int TourId { get; set; }

// Foreign Keys
        public int UserId { get; set; }
        public int LegId { get; set; }

// Nav Props
        public User Users { get; set; }
        public Leg Legs { get; set; }
        }
}